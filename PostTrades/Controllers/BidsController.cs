using Microsoft.AspNetCore.Mvc;
using PostTrades.Domain;
using PostTrades.Repositories;

namespace PostTrades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IBidRepository _bidRepository;
        private readonly ILogger<BidsController> _logger;

        public BidsController(IBidRepository bidRepository, ILogger<BidsController> logger)
        {
            _bidRepository = bidRepository ?? throw new ArgumentNullException(nameof(bidRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieve all Bids.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Bid>>> GetAllBids()
        {
            try
            {
                var bids = await _bidRepository.GetAllAsync();

                if (bids == null)
                {
                    _logger.LogInformation("No Bid found");

                return NotFound();
                }

                _logger.LogInformation($"List of Bids retrieved successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return Ok(bids);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Bids");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retrieve a specific Bid by ID.
        /// </summary>
        /// <param name="id">Bid ID</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Bid>> GetBidById(int id)
        {
            try
            {
                var bid = await _bidRepository.GetByIdAsync(id);

                if (bid == null)
                {
                    _logger.LogInformation($"Bid {id} not found");

                    return NotFound();
                }

                _logger.LogInformation($"Bid {id} retrieved successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return Ok(bid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving Bid {id}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Create a new Bid.
        /// </summary>
        /// <param name="bid">Bid object</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Bid>> CreateBid(Bid bid)
        {
            try
            {
                if (bid == null)
                {
                    _logger.LogError("Invalid Bid data");

                    return BadRequest();
                }

                await _bidRepository.CreateAsync(bid);
                _logger.LogInformation($"New Bid created successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return CreatedAtAction(nameof(GetBidById), new { id = bid.BidId }, bid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new Bid");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update a Bid.
        /// </summary>
        /// <param name="id">Bid ID</param>
        /// <param name="bid">Bid object</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateBid(int id, Bid bid)
        {
            try
            {
                if (id != bid?.BidId)
                {
                    _logger.LogError($"Bid {id} mismatch");

                    return BadRequest();
                }

                await _bidRepository.UpdateAsync(bid);
                _logger.LogInformation($"Bid {id} updated successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating Bid {id}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Delete a Bid by ID.
        /// </summary>
        /// <param name="id">Bid ID</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteBid(int id)
        {
            try
            {
                await _bidRepository.DeleteAsync(id);
                _logger.LogInformation($"Bid {id} deleted successfully at {DateTime.UtcNow.ToLongTimeString() + 1}");

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting Bid {id}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
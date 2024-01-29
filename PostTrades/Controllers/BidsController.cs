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

        public BidsController(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }
        #region GET ALL
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Bid>>> GetAllBids()
        {
            var bids = await _bidRepository.GetAllAsync();

            return Ok(bids);
        }
        #endregion
        #region GET BY ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Bid>> GetBidById(int id)
        {
            var bid = await _bidRepository.GetByIdAsync(id);

            return Ok(bid);
        }
        #endregion
        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Bid>> CreateBid(Bid bid)
        {
            await _bidRepository.CreateAsync(bid);

            return CreatedAtAction("GetBidById", new { id = bid.BidId }, bid);
        }
        #endregion
        #region PUT
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateBid(int id, Bid bid)
        {
            await _bidRepository.UpdateAsync(bid);

            return NoContent();
        }
        #endregion
        #region DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteBid(int id)
        {
            await _bidRepository.DeleteAsync(id);

            return NoContent();
        }
        #endregion
    }
}
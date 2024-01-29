using Microsoft.AspNetCore.Mvc;
using PostTrades.Domain;
using PostTrades.Repositories;

namespace PostTrades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingsController(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
        #region GET ALL
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Rating>>> GetAllRatings()
        {
            var ratings = await _ratingRepository.GetAllAsync();

            return Ok(ratings);
        }
        #endregion
        #region GET BY ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Rating>> GetRatingById(int id)
        {
            var rating = await _ratingRepository.GetByIdAsync(id);

            return Ok(rating);
        }
        #endregion
        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Rating>> CreateRating(Rating rating)
        {
            await _ratingRepository.CreateAsync(rating);

            return CreatedAtAction("GetRatingById", new { id = rating.RatingId }, rating);
        }
        #endregion
        #region PUT
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateRating(int id, Rating rating)
        {
            await _ratingRepository.UpdateAsync(rating);

            return NoContent();
        }
        #endregion
        #region DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteRating(int id)
        {
            await _ratingRepository.DeleteAsync(id);

            return NoContent();
        }
        #endregion
    }
}
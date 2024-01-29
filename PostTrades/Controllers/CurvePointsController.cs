using Microsoft.AspNetCore.Mvc;
using PostTrades.Domain;
using PostTrades.Repositories;

namespace PostTrades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurvePointsController : ControllerBase
    {
        private readonly ICurvePointRepository _curvePointRepository;

        public CurvePointsController(ICurvePointRepository curvePointRepository)
        {
            _curvePointRepository = curvePointRepository;
        }
        #region GET ALL
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CurvePoint>>> GetAllCurvePoints()
        {
            var curvePoints = await _curvePointRepository.GetAllAsync();

            return Ok(curvePoints);
        }
        #endregion
        #region GET BY ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CurvePoint>> GetCurvePointById(int id)
        {
            var curvePoint = await _curvePointRepository.GetByIdAsync(id);

            return Ok(curvePoint);
        }
        #endregion
        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CurvePoint>> CreateCurvePoint(CurvePoint curvePoint)
        {
            await _curvePointRepository.CreateAsync(curvePoint);

            return CreatedAtAction("GetCurvePointById", new { id = curvePoint.CurvePointId }, curvePoint);
        }
        #endregion
        #region PUT
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateCurvePoint(int id, CurvePoint curvePoint)
        {
            await _curvePointRepository.UpdateAsync(curvePoint);

            return NoContent();
        }
        #endregion
        #region DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCurvePoint(int id)
        {
            await _curvePointRepository.DeleteAsync(id);

            return NoContent();
        }
        #endregion
    }
}
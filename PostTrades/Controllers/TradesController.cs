using Microsoft.AspNetCore.Mvc;
using PostTrades.Domain;
using PostTrades.Repositories;

namespace PostTrades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private readonly ITradeRepository _tradeRepository;

        public TradesController(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }
        #region GET ALL
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Trade>>> GetAllTrades()
        {
            var trades = await _tradeRepository.GetAllAsync();

            return Ok(trades);
        }
        #endregion
        #region GET BY ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Trade>> GetTradeById(int id)
        {
            var trade = await _tradeRepository.GetByIdAsync(id);

            return Ok(trade);
        }
        #endregion
        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Trade>> CreateTrade(Trade trade)
        {
            await _tradeRepository.CreateAsync(trade);

            return CreatedAtAction("GetTradeById", new { id = trade.TradeId }, trade);
        }
        #endregion
        #region PUT
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateTrade(int id, Trade trade)
        {
            await _tradeRepository.UpdateAsync(trade);

            return NoContent();
        }
        #endregion
        #region DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteTrade(int id)
        {
            await _tradeRepository.DeleteAsync(id);

            return NoContent();
        }
        #endregion
    }
}
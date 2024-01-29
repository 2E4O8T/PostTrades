using Microsoft.AspNetCore.Mvc;
using PostTrades.Domain;
using PostTrades.Repositories;

namespace PostTrades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleNamesController : ControllerBase
    {
        private readonly IRuleNameRepository _ruleNameRepository;

        public RuleNamesController(IRuleNameRepository ruleNameRepository)
        {
            _ruleNameRepository = ruleNameRepository;
        }
        #region GET ALL
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RuleName>>> GetAllRuleNames()
        {
            var ruleNames = await _ruleNameRepository.GetAllAsync();

            return Ok(ruleNames);
        }
        #endregion
        #region GET BY ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RuleName>> GetRuleNameById(int id)
        {
            var ruleName = await _ruleNameRepository.GetByIdAsync(id);

            return Ok(ruleName);
        }
        #endregion
        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RuleName>> CreateRuleName(RuleName ruleName)
        {
            await _ruleNameRepository.CreateAsync(ruleName);

            return CreatedAtAction("GetRuleNameById", new { id = ruleName.RuleNameId }, ruleName);
        }
        #endregion
        #region PUT
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateRuleName(int id, RuleName ruleName)
        {
            await _ruleNameRepository.UpdateAsync(ruleName);

            return NoContent();
        }
        #endregion
        #region DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteRuleName(int id)
        {
            await _ruleNameRepository.DeleteAsync(id);

            return NoContent();
        }
        #endregion
    }
}
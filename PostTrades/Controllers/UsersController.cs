using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostTrades.Domain;
using PostTrades.Repositories;

namespace PostTrades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #region GET ALL
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();

            return Ok(users);
        }
        #endregion
        #region GET BY ID
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return Ok(user);
        }
        #endregion
        #region DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteAsync(id);

            return NoContent();
        }
        #endregion
    }
}
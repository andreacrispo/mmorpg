using Microsoft.AspNetCore.Mvc;
using MMORPG.Domain;
using MMORPG.Domain.DTO;
using MMORPG.Service;

namespace MMORPG.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {


        private readonly ILogger<UserController> _logger;
        private readonly IUserService userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            this.userService = userService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> GetCharacter([FromBody] LoginRequest request)
        {
            bool result = await userService.Login(request.username, request.characterClass);

            if (!result)
                return BadRequest();

            return Ok();
        }


    }
}

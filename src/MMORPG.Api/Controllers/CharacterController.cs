using Microsoft.AspNetCore.Mvc;
using MMORPG.Domain;
using MMORPG.Service;

namespace MMORPG.Controllers
{
    [ApiController]
    [Route("api/character")]
    public class CharacterController : ControllerBase
    {
        private readonly ILogger<CharacterController> _logger;
        private readonly ICharacterService characterService;

        public CharacterController(ILogger<CharacterController> logger, ICharacterService characterService)
        {
            _logger = logger;
            this.characterService = characterService;
        }

        [HttpGet]
        [Route("{characterId}")]
        public async Task<ActionResult<Character>> GetCharacter(int characterId)
        {
            Character? character = await this.characterService.GetCharacter(characterId);
            if (character == null)
                return BadRequest();

            return character;
        }

        [HttpGet]
        [Route("{characterId}/position")]
        public async Task<ActionResult<Position>> GetCharacterPosition(int characterId)
        {
            Character? character = await this.characterService.GetCharacter(characterId);
            if (character == null)
                return NotFound();

            return character.Position;
        }


        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Character>>> GetCharacterList()
        {
            return await this.characterService.GetCharacters();
        }

        [HttpGet]
        [Route("all/alive")]
        public async Task<ActionResult<List<Character>>> getCharacterAliveList()
        {
            return await this.characterService.GetCharactersAlive();
        }

        [HttpGet]
        [Route("all/connected")]
        public async Task<ActionResult<List<Character>>> getCharacterConnectedList()
        {
            return await this.characterService.GetCharactersConnected();
        }

        [HttpGet]
        [Route("{characterId}/attack/{targetId}")]
        public async Task<ActionResult> attackCharacter(int characterId, int targetId)
        {
            await characterService.Attack(characterId, targetId);

            return Ok();
        }

        [HttpGet]
        [Route("respawn/{characterId}")]
        public async Task<ActionResult> respawn(int characterId)
        {
            if (await this.characterService.Respawn(characterId))
                return Ok();

            return BadRequest();

        }
    }
}

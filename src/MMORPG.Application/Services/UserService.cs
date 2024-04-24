using MMORPG.Domain;
using MMORPG.Domain.Entity;
using MMORPG.Repository;

namespace MMORPG.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly ICharacterService characterService;

        public UserService(IUserRepository repository, ICharacterService characterService)
        {
            this.repository = repository;
            this.characterService = characterService;
        }

        public async Task<bool> Login(string username, CharacterClass characterClass)
        {
            UserEntity? user = await this.repository.FindByUsername(username);
            if (user == null)
                return false;

            Character? character = await this.GetCharacterByUsernameAndClassId(user, characterClass);
            if (character == null)
                character = CreateCharacterByUsernameAndClass(user, characterClass);

            if (character == null)
                return false;

            return await this.characterService.Connect(character);
        }

        public async Task<bool> Logout(string username)
        {
            UserEntity? user = await this.repository.FindByUsername(username);
            if (user == null)
                return false;

            IList<Character> characters = await this.characterService.GetCharactersByUser(user);
            if (characters == null || characters.Count == 0)
                return false;

            foreach (Character character in characters)
            {
                await characterService.Disconnect(character);
            }

            return !characters.Any(c => c.IsConnected);

        }

        private async Task<Character?> GetCharacterByUsernameAndClassId(UserEntity user, CharacterClass characterClass)
        {
            IList<Character> characters = await characterService.GetCharactersByUser(user);
            IList<Character> selectedClassChars = characters.Where(c => c.CharacterClass == characterClass).ToList();

            return selectedClassChars.FirstOrDefault();
        }

        private Character CreateCharacterByUsernameAndClass(UserEntity user, CharacterClass characterClass)
        {

            return this.characterService.CreateCharacter(user, characterClass);
        }
    }
}

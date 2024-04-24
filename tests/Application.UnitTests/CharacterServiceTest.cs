using MMORPG.Domain.Entity;
using MMORPG.Domain;
using MMORPG.Service;
using MMORPG.Repository;
using Moq;
using System.Linq.Expressions;

namespace MMORPG.Test.Service
{
    public class CharacterServiceTest
    {

        private ICharacterService service;
        private CharacterFactory factory;

        [SetUp]
        public void Setup()
        {
            CharacterEntity c1 = new CharacterEntity()
            {
                Id = 1,
                Hp = 50,
                IsConnected = true,
                LevelValue = 1,
                ClassId = CharacterClass.Paladin
            };
            CharacterEntity c2 = new CharacterEntity()
            {
                Id = 2,
                Hp = 100,
                IsConnected = true,
                LevelValue = 1,
                ClassId = CharacterClass.Wizard
            };
            CharacterEntity c3 = new CharacterEntity()
            {
                Id = 3,
                Hp = 150,
                IsConnected = true,
                LevelValue = 1,
                ClassId = CharacterClass.Rogue
            };

            CharacterEntity c3Pos = new CharacterEntity()
            {
                Id = 3,
                Hp = 150,
                IsConnected = true,
                LevelValue = 1,
                PositionX = 99,
                PositionY = 55,
                MoveDirection = MoveDirection.Down,
                ClassId = CharacterClass.Rogue
            };

            Task<List<CharacterEntity>> taskResult = Task.FromResult(new List<CharacterEntity> { c1, c2, c3 });
            CharacterEntity cNull = null;

            Mock<ICharacterRepository> mockRepository = new Mock<ICharacterRepository>();

            factory = new CharacterFactory();
            service = new CharacterService(mockRepository.Object, factory);
            mockRepository.Setup(r => r.GetAll()).Returns(taskResult);
            mockRepository.Setup(r => r.GetWhere(It.IsAny<Expression<Func<CharacterEntity, bool>>>())).Returns(taskResult);
            mockRepository.Setup(r => r.FindById(3)).Returns(Task.FromResult(c3));
            mockRepository.Setup(r => r.FindById(9999)).Returns(Task.FromResult(cNull));

        }

        [Test]
        public async Task
        ensure_getCharacters_does_not_return_null()
        {
            List<Character> characters = await this.service.GetCharacters();
            Assert.IsNotNull(characters);
        }


        [Test]
        public async Task
        ensure_that_get_alive_characters_returns_only_alive_characters()
        {
            List<Character> aliveCharacters = await this.service.GetCharactersAlive();

            Assert.IsNotNull(aliveCharacters);
            Assert.True(aliveCharacters.Count > 0 && aliveCharacters.All(x => x.IsAlive));
        }


        [Test]
        public async Task
        ensure_that_get_connected_characters_returns_only_connected_characters()
        {
            List<Character> connectedCharacters = await this.service.GetCharactersConnected();

            Assert.IsNotNull(connectedCharacters);
            Assert.True(connectedCharacters.Count > 0 && connectedCharacters.All(x => x.IsConnected));
        }

        [Test]
        public async Task
        ensure_get_character_of_existing_character_returns_character_data()
        {
            Character character = await this.service.GetCharacter(3);

            Assert.That(character != null, Is.True);



            Assert.AreEqual(3, character.Id);
            Assert.AreEqual(150, character.Hp);
            Assert.AreEqual(1, character.Level);
            Assert.True(character.IsConnected);
        }

        [Test]
        public async Task
        ensure_get_character_of_not_existing_character_returns_empty()
        {
            Character characterOptional = await this.service.GetCharacter(999);
            Assert.That(characterOptional, Is.EqualTo(null));
        }




    }
}

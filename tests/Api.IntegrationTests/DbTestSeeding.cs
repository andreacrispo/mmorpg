using MMORPG.Domain.Entity;
using MMORPG.Domain;
using MMORPG.Infrastrutture;
using Microsoft.EntityFrameworkCore;

namespace Api.IntegrationTests;
public class DbTestSeeding
{


    public static void InitDb(MMORPGDbContext testDbContext)
    {

        testDbContext.Database.Migrate(); // Apply Migrations


        var u = testDbContext.Users.FirstOrDefault(x => x.Id == 1);


        List<CharacterEntity> characterEntities = new List<CharacterEntity>
        {
      new CharacterEntity() { Id = 100, PositionX = 8, PositionY = 2, PositionZ = 0, LevelValue = 1, ClassId = CharacterClass.Paladin, User = u },
      new CharacterEntity() { Id = 200, PositionX = 7, PositionY = 3, PositionZ = 0, LevelValue = 1, ClassId = CharacterClass.Wizard, User = u },
      new CharacterEntity() { Id = 300, PositionX = 11, PositionY = 11, PositionZ = 0, LevelValue = 1, ClassId = CharacterClass.Rogue, User = u },

      new CharacterEntity() { Id = 999, PositionX = 0, PositionY = 0, PositionZ = 0, LevelValue = 1, ClassId = CharacterClass.Rogue, User = u },

       // To attack in range
      new CharacterEntity() { Id = 1001, PositionX = 5, PositionY = 5, PositionZ = 0,LevelValue = 1, ClassId = CharacterClass.Paladin, Hp = 150, User = u },
      new CharacterEntity() { Id = 1002, PositionX = 6, PositionY = 6,PositionZ = 0, LevelValue = 1, ClassId = CharacterClass.Rogue, Hp = 120, User = u },
       // To attack not in range
      new CharacterEntity() { Id = 1003, PositionX = 5, PositionY = 5,PositionZ = 0, LevelValue = 1, ClassId = CharacterClass.Rogue, Hp = 150, User = u },
      new CharacterEntity() { Id = 1004, PositionX = 300, PositionY = 300, PositionZ = 0,LevelValue = 1, ClassId = CharacterClass.Wizard, Hp = 120, User = u },
       //Alive
      new CharacterEntity() { Id = 2001, PositionX = 300, PositionY = 300,PositionZ = 0, LevelValue = 1, ClassId = CharacterClass.Rogue, User = u, Hp = 120 },
       // Dead
      new CharacterEntity() { Id = 2002, PositionX = 300, PositionY = 300,PositionZ = 0, LevelValue = 1, ClassId = CharacterClass.Rogue, User = u, Hp = 0 },

        };

        testDbContext.Characters.AddRange(characterEntities);
        //testDbContext.Characters.Add(new CharacterEntity() { Id = 100, PositionX = 8, PositionY = 2, LevelValue = 1, ClassId = CharacterClass.Paladin, User = u });
        //testDbContext.Characters.Add(new CharacterEntity() { Id = 200, PositionX = 7, PositionY = 3, LevelValue = 1, ClassId = CharacterClass.Wizard, User = u });
        //testDbContext.Characters.Add(new CharacterEntity() { Id = 300, PositionX = 11, PositionY = 11, LevelValue = 1, ClassId = CharacterClass.Rogue, User = u });

        //testDbContext.Characters.Add(new CharacterEntity() { Id = 999, PositionX = 0, PositionY = 0, LevelValue = 1, ClassId = CharacterClass.Rogue, User = u });

        //// To attack in range
        //testDbContext.Characters.Add(new CharacterEntity() { Id = 1001, PositionX = 5, PositionY = 5, LevelValue = 1, ClassId = CharacterClass.Rogue, Hp = 150, User = u });
        //testDbContext.Characters.Add(new CharacterEntity() { Id = 1002, PositionX = 6, PositionY = 6, LevelValue = 1, ClassId = CharacterClass.Wizard, Hp = 120, User = u });
        //// To attack not in range
        //testDbContext.Characters.Add(new CharacterEntity() { Id = 1003, PositionX = 5, PositionY = 5, LevelValue = 1, ClassId = CharacterClass.Rogue, Hp = 150, User = u });
        //testDbContext.Characters.Add(new CharacterEntity() { Id = 1004, PositionX = 300, PositionY = 300, LevelValue = 1, ClassId = CharacterClass.Wizard, Hp = 120, User = u });
        ////Alive
        //testDbContext.Characters.Add(new CharacterEntity() { Id = 2001, PositionX = 300, PositionY = 300, LevelValue = 1, ClassId = CharacterClass.Rogue, User = u, Hp = 120 });
        //// Dead
        //testDbContext.Characters.Add(new CharacterEntity() { Id = 2002, PositionX = 300, PositionY = 300, LevelValue = 1, ClassId = CharacterClass.Rogue, User = u, Hp = 0 });


        //      (id, level_value, is_connected, positionx, positiony, positionz, rotationx, rotationy, rotationz, rotation_amount, hp, class_id) VALUES
        //(100,1, 1, 8, 2, 1, 0, 0, 0, 0, 150, 1),
        //(200, 2, 0, 7, 3, 1, 0, 0, 0, 0, 100, 2),
        //(300, 2, 0, 11, 11, 1, 0, 0, 0, 0, 120, 3),
        //---To update position
        //(999,2, 0, 0, 0, 1, 0, 0, 0, 0, 120, 3),
        //---To attack in range
        //(1001, 2, 0, 5, 5, 1, 0, 0, 0, 0, 150, 1),
        //(1002, 2, 0, 6, 6, 1, 0, 0, 0, 0, 120, 2),
        //---To attack not in range
        //(1003, 2, 0, 5, 5, 1, 0, 0, 0, 0, 150, 1),
        //(1004, 2, 0, 300, 300, 1, 0, 0, 0, 0, 120, 2),
        //--Only for test alive dont modify
        //(2001, 2, 0, 300, 300, 1, 0, 0, 0, 0, 120, 2),
        //--Only for test dead dont modify
        //(2002, 2, 0, 300, 300, 1, 0, 0, 0, 0, 0, 2);

        testDbContext.SaveChanges();
    }

}

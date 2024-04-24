using MMORPG.Domain;

namespace MMORPG
{
    class RogueTest
    {
        public class RogueRandomDataMocked : RandomDataGeneratorInterface
        {

            public int GetRandomPercentage()
            {
                return 20;
            }

            public int GetRandomValueRange(int minInclude, int maxInclude)
            {
                return 9;
            }
        }

        private Rogue character;


        [SetUp]
        public void Init()
        {
            character = new Rogue(new RogueRandomDataMocked());
        }


        [Test]
        public void Is_new_rogue_successfully_initialized()
        {
            Assert.AreEqual(120, character.Hp);
            Assert.AreEqual(1, character.Level);
            Assert.AreEqual(3, character.Resistence);
        }
        [Test]
        public void Ensure_that_rogue_damage_is_valid()
        {
            int damage = character.AttackDamage();
            Assert.True(damage >= 9 && damage <= 12, "not in range");
        }
        [Test]
        public void Ensure_that_rogue_empowered_damage_is_valid([Values(1, 2)] int power)
        {
            character.Level = power;
            int empoweredDamage = character.EmpoweredDamage();
            Assert.True(empoweredDamage >= (9 * character.Level));
            Assert.True(empoweredDamage <= (12 * character.Level));
        }
        [Test]
        public void Ensure_that_rogue_with_invalid_power_throws_exception([Values(-1, 0)] int power)
        {
            Assert.Throws<ArgumentException>(() => character.Level = power);
        }


        //      [Test]TODO:FIXME
        //public  void Ensure_that_have_20_perc_chance_double_damages_each_attack()
        //{
        //    Character paladin = new Paladin();
        //    double normalDamage = (character.EmpoweredDamage() * character.GetSpecialDamage(paladin)) / paladin.Resistence;
        //    double doubledDamage = character.CalculateTotalDamage(paladin);
        //    Assert.AreEqual(doubledDamage, normalDamage * 2);
        //}
    }
}
using MMORPG.Domain;


namespace MMORPG
{
    class PaladinTest
    {
        public class PaladinRandomDataMocked : RandomDataGeneratorInterface
        {
            public virtual int GetRandomPercentage()
            {
                return 20;
            }

            public virtual int GetRandomValueRange(int minInclude, int maxInclude)
            {
                return 5;
            }
        }

        private Paladin character;

        [SetUp]
        public void Init()
        {
            character = new Paladin(new PaladinRandomDataMocked());
        }

        [Test]
        public void Is_new_paladin_successfully_initialized()
        {
            Assert.AreEqual(150, this.character.Hp);
            Assert.AreEqual(1, character.Level);
            Assert.AreEqual(4, character.Resistence);
        }

        [Test]
        public void Ensure_that_paladins_damage_is_valid()
        {
            int damage = character.AttackDamage();
            Assert.True(damage >= 5 && damage <= 8, "not in range");
        }

        [Test]
        public void Ensure_that_paladin_empowered_damage_is_valid([Values(1, 2)] int power)
        {
            character.Level = power;
            int empoweredDamage = character.EmpoweredDamage();
            Assert.True(empoweredDamage >= (5 * character.Level));
            Assert.True(empoweredDamage <= (8 * character.Level));
        }

        [Test]
        public void Ensure_that_paladin_with_invalid_power_throws_exception([Values(-1, 0)] int power)
        {
            Assert.Throws<ArgumentException>(() => character.Level = power);
        }

        [Test]
        public void Ensure_resistance_doubles_when_defends()
        {
            Wizard wizard = new Wizard();
            double initialResistance = character.Resistence;
            wizard.Attack(character);
            Assert.AreEqual(initialResistance * 2, character.Resistence);
        }
    }
}
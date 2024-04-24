using MMORPG.Domain;


namespace MMORPG
{
    class WizardTest
    {
        public class WizardRandomDataMocked : RandomDataGeneratorInterface
        {

            public int GetRandomPercentage()
            {
                return 20;
            }


            public int GetRandomValueRange(int minInclude, int maxInclude)
            {
                return 13;
            }
        }

        private Wizard wizard;

        [SetUp]
        public void Init()
        {
            wizard = new Wizard(new WizardRandomDataMocked());
        }

        [Test]
        public void Is_new_wizard_successfully_initialized()
        {
            Assert.AreEqual(100, wizard.Hp);
            Assert.AreEqual(1, wizard.Level);
            Assert.AreEqual(2, wizard.Resistence);
        }

        [Test]
        public void Ensure_that_wizard_damage_is_valid()
        {
            int damage = wizard.AttackDamage();
            Assert.True(damage >= 13 && damage <= 16, "not in range");
        }

        [Test]
        public void Ensure_that_wizard_empowered_damage_is_valid([Values(1, 2)] int power)
        {
            wizard.Level = power;
            int empoweredDamage = wizard.EmpoweredDamage();
            Assert.True(empoweredDamage >= (13 * wizard.Level));
            Assert.True(empoweredDamage <= (16 * wizard.Level));
        }

        [Test]
        public void Ensure_that_wizard_with_invalid_power_throws_exception([Values(-1, 0)] int power)
        {
            Assert.Throws<ArgumentException>(() => wizard.Level = power);
        }

        [Test]
        public void Ensure_increase_hp_by_invalid_percent_is_ignored()
        {
            double initialHps = wizard.Hp;
            wizard.IncreaseHpByPercentage(-10);
            Assert.AreEqual(initialHps, wizard.Hp);
        }

        [Test]
        public void Ensure_hps_increase_by_correct_percentage_on_attack()
        {
            Paladin paladin = new Paladin();
            wizard.Defend(30);
            double remainingHps = wizard.Hp;
            wizard.Attack(paladin);
            double tenPercentHps = remainingHps * 0.1;
            Assert.AreEqual(wizard.Hp, (remainingHps + tenPercentHps));
        }


        // TODO: FIXME
        //public  void Ensure_hps_remain_same_when_attack_rogue()
        //{
        //    Rogue rogue = new Rogue();
        //    Assert.AreEqual(1, wizard.GetSpecialDamage(rogue));
        //}
    }
}
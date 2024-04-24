using MMORPG.Domain;

namespace MMORPG
{
    class PropTest
    {
        [Test]
        public void Ensure_that_when_reduced_to_0_health_things_are_destroyed()
        {
            Prop prop = new TreeProp();
            prop.Defend(prop.Hp);
            Assert.True(prop.IsDestroyed);
        }

        [Test]
        public void Ensure_that_prop_can_be_attack()
        {
            Prop prop = new TreeProp();
            Character paladin = new Paladin();
            paladin.Attack(prop);
            Assert.True(prop.Hp < prop.InitHp);
        }
    }
}
namespace MMORPG.Domain
{
    public class Paladin : Character
    {
        public Paladin() : this(new RandomDataGenerator())
        {
        }

        public Paladin(RandomDataGeneratorInterface proc)
        {
            this.characterClass = CharacterClass.Paladin;
            this.randomDataGenerator = proc;
            this.hp = 150;
            this.initHp = hp;
            this.resistance = 4;
            this.maxRange = 200;
            this.position = Position.At(0, 0, 0);
            this.rotation = Rotation.At(0, 0, 0);
            this.level = 1;
        }

        public override int AttackDamage()
        {
            return randomDataGenerator.GetRandomValueRange(5, 8);
        }

        protected override double GetSpecialDamage(Character enemy)
        {
            if (enemy is Rogue)
                return 1.5;
            return 1;
        }

        protected override double CalculateReceivedDamage(double damage)
        {
            if (CanProc())
                this.DoubleResistance();
            return base.CalculateReceivedDamage(damage);
        }

        protected override double CalculateHealingHps()
        {
            return randomDataGenerator.GetRandomValueRange(3, 6);
        }

        private void DoubleResistance()
        {
            this.resistance *= 2;
        }

        private bool CanProc()
        {
            return randomDataGenerator.GetRandomPercentage() <= 20;
        }
    }
}

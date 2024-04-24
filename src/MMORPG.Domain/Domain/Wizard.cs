namespace MMORPG.Domain
{
    public class Wizard : Character
    {
        public Wizard() : this(new RandomDataGenerator())
        {
        }

        public Wizard(RandomDataGeneratorInterface proc)
        {
            this.characterClass = CharacterClass.Wizard;
            this.randomDataGenerator = proc;
            this.hp = 100;
            this.initHp = hp;
            this.resistance = 2;
            this.maxRange = 1000;
            this.position = Position.At(0, 0);
            this.Level = 1;
        }

        public override int AttackDamage()
        {
            return randomDataGenerator.GetRandomValueRange(13, 16);
        }

        protected override double GetSpecialDamage(Character enemy)
        {
            if (enemy is Paladin)
                return 1.5;
            return 1;
        }

        public override double CalculateTotalDamage(Character target)
        {
            double totalDamage = base.CalculateTotalDamage(target);
            if (CanProc())
                this.IncreaseHpByPercentage(10);
            return totalDamage;
        }

        protected override double CalculateHealingHps()
        {
            return randomDataGenerator.GetRandomValueRange(1, 4);
        }

        public virtual void IncreaseHpByPercentage(double percentage)
        {
            if (percentage < 0)
                return;
            this.Healing(this.hp * (percentage / 100));
        }

        private bool CanProc()
        {
            return randomDataGenerator.GetRandomPercentage() <= 20;
        }
    }
}
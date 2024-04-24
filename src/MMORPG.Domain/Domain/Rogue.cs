namespace MMORPG.Domain
{
    public class Rogue : Character
    {
        public Rogue() : this(new RandomDataGenerator())
        {
        }

        public Rogue(RandomDataGeneratorInterface randomDataGenerator)
        {
            this.characterClass = CharacterClass.Rogue;
            this.randomDataGenerator = randomDataGenerator;
            this.hp = 120;
            this.initHp = hp;
            this.resistance = 3;
            this.maxRange = 100;
            this.position = Position.At(0, 0);
            this.Level = 1;
        }

        public override int AttackDamage()
        {
            return randomDataGenerator.GetRandomValueRange(9, 12);
        }

        protected override double GetSpecialDamage(Character enemy)
        {
            if (enemy is Wizard)
                return 1.5;
            return 1;
        }

        public override double CalculateTotalDamage(Character target)
        {
            double damage = base.CalculateTotalDamage(target);
            if (this.CanProc())
                damage = damage * 2;
            return damage;
        }

        protected override double CalculateHealingHps()
        {
            return 0;
        }

        private bool CanProc()
        {
            return randomDataGenerator.GetRandomPercentage() <= 20;
        }
    }
}
namespace MMORPG.Domain
{
    public abstract class Target
    {
        protected double initHp = 0;
        protected double hp = 0;

        public double Hp
        {
            get => hp;
            set => hp = value;
        }

        public virtual double InitHp => initHp;

        public virtual void Defend(double damage)
        {
            if (damage <= 0)
                return;
            this.hp -= this.CalculateReceivedDamage(damage);
            if (this.hp <= 0)
                this.hp = 0;
        }

        protected virtual double CalculateReceivedDamage(double damage)
        {
            return damage;
        }
    }
}
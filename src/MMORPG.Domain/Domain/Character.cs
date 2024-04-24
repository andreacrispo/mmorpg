
namespace MMORPG.Domain
{
    public abstract class Character : Target
    {
        protected int id;
        protected int level;
        protected int resistance;
        protected double maxRange;
        protected bool isConnected;
        protected string username;
        protected Faction? faction;
        protected Position position;
        protected MoveDirection moveDirection = MoveDirection.None;
        protected RandomDataGeneratorInterface randomDataGenerator;
        protected CharacterClass characterClass;

        public string Username
        {
            get => username;
            set => username = value;
        }

        public int Level
        {
            get => level;
            set
            {
                if (value <= 0) throw new ArgumentException("Invalid level");
                level = value;
            }
        }

        public bool IsConnected
        {
            get => isConnected;
            set => isConnected = value;
        }

        public Faction? Faction
        {
            get => faction;
            set => faction = value;
        }

        public int Resistence
        {
            get => resistance;
            set => resistance = value;
        }


        public Position Position
        {
            get => position;
            set => position = value;
        }


        public int Id
        {
            get => id;
            set => id = value;
        }

        public CharacterClass CharacterClass
        {
            get => characterClass;
        }

        public MoveDirection MoveDirection
        {
            get => moveDirection;
            set => moveDirection = value;
        }
        public bool IsDead => this.hp == 0;

        public bool IsAlive => this.hp > 0;


        protected abstract double GetSpecialDamage(Character enemy);

        public abstract int AttackDamage();

        public virtual int EmpoweredDamage()
        {
            return this.AttackDamage() * this.level;
        }


        public virtual void Attack(Character target)
        {
            if (!CanAttack(target))
                return;

            double totalDamage = this.CalculateTotalDamage(target);
            target.Defend(totalDamage);
        }

        public virtual void Attack(Prop target)
        {
            double totalDamage = this.CalculateTotalDamage(target);
            target.Defend(totalDamage);
        }

        private bool CanAttack(Character character)
        {
            double enemyDistance = this.position.DistanceFrom(character.Position);
            return this.IsAlive
                && enemyDistance <= maxRange
                && !character.IsAlly(this)
                && !character.IsMe(this);
        }

        public virtual double CalculateTotalDamage(Character target)
        {
            double totalDamage = (this.EmpoweredDamage() * this.GetSpecialDamage(target)) / target.Resistence;
            if (target.level - this.level >= 5)
                totalDamage *= 0.5;
            else if (this.level - target.level >= 5)
                totalDamage += totalDamage * 0.5;
            return totalDamage;
        }

        public virtual double CalculateTotalDamage(Prop target)
        {
            return this.EmpoweredDamage();
        }

        public virtual void Heal(Character target)
        {
            if (!CanHeal(target))
                return;
            double hps = this.CalculateHealingHps();
            target.Healing(hps);
        }

        private bool CanHeal(Character target)
        {
            return (this.IsAlive && target.IsAlive) && (this.IsMe(target) || target.IsAlly(this));
        }

        protected abstract double CalculateHealingHps();

        protected virtual void Healing(double hps)
        {
            this.hp = Math.Min(this.hp + hps, this.initHp);
        }


        public virtual void JoinFaction(Faction faction)
        {
            if (this.faction != null)
                return;
            this.faction = faction;
        }

        public virtual void LeaveFaction()
        {
            this.faction = null;
        }

        public virtual bool IsAlly(Character character)
        {
            return this.faction != null && this.faction.Equals(character.faction);
        }

        private bool IsMe(Character character)
        {
            return this.Equals(character);
        }
 
    }
}

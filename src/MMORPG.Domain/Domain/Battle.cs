namespace MMORPG.Domain
{
    public class Battle
    {
        private readonly Character characterOne;
        private readonly Character characterTwo;
        public Battle(Character c1, Character c2)
        {
            this.characterOne = c1;
            this.characterTwo = c2;
        }

        public virtual void Fight()
        {
            characterOne.Attack(characterTwo);
            characterTwo.Attack(characterOne);
        }
    }
}
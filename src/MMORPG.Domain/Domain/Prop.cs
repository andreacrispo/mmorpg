namespace MMORPG.Domain
{
    public abstract class Prop : Target
    {
        public bool IsDestroyed
        {
            get => this.hp == 0;
        }
    }
}
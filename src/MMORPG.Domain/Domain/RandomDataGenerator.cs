
namespace MMORPG.Domain
{
    public class RandomDataGenerator : RandomDataGeneratorInterface
    {
        private readonly Random _random;

        public RandomDataGenerator()
        {
            _random = new Random();
        }
        public virtual int GetRandomPercentage()
        {
            return _random.Next(1, 101);
        }

        public virtual int GetRandomValueRange(int minInclude, int maxInclude)
        {
            return _random.Next(minInclude, maxInclude + 1);
        }
    }
}
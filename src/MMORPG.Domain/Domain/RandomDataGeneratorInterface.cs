namespace MMORPG.Domain
{
    public interface RandomDataGeneratorInterface
    {
        int GetRandomPercentage();
        int GetRandomValueRange(int minInclude, int maxInclude);
    }
}
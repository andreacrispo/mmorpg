

namespace MMORPG.Domain
{
    public class Position
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Position(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Position At(double x, double y)
        {
            return new Position(x, y);
        }

        public double DistanceFrom(Position otherPosition)
        {
            double xDiff = otherPosition.X - this.X;
            double yDiff = otherPosition.Y - this.Y;
            return Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
        }
    }
}
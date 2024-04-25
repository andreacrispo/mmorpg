

namespace MMORPG.Domain
{
    public class Position
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Position(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static Position At(double x, double y, double z)
        {
            return new Position(x, y, z);
        }

        public double DistanceFrom(Position otherPosition)
        {
            double xDiff = otherPosition.X - this.X;
            double yDiff = otherPosition.Y - this.Y;
            double zDiff = otherPosition.Z- this.Z;
            return Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2) + Math.Pow(zDiff, 2));
        }
    }
}

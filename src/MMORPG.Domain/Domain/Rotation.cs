namespace MMORPG.Domain;
public class Rotation
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Rotation(double x, double y, double z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public static Rotation At(double x, double y, double z)
    {
        return new Rotation(x, y, z);
    }
}

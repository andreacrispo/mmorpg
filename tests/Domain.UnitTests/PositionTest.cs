using MMORPG.Domain;


namespace MMORPG
{
    public class PositionTest
    {
        [Test]
        public void Ensure_calculated_distance_from_two_same_point_is_zero()
        {
            Position position1 = new Position(13, 13, 0);
            Position position2 = new Position(13, 13, 0);
            double distance = position1.DistanceFrom(position2);
            Assert.AreEqual(0, distance);
        }

        [Test]
        public void Ensure_calculated_distance_from_point_is_valid()
        {
            Position position1 = new Position(10, 0, 0);
            Position position2 = new Position(5, 0, 0);
            double distance = position1.DistanceFrom(position2);
            Assert.AreEqual(5, distance);
        }
    }
}

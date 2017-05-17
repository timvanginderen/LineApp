namespace MijnLijn.Utils
{
    public class MathUtil
    {
        private const double PiDividedBy180 = 0.0174532925199432958;

        public static double ToRadians(double degrees)
        {
            return degrees * PiDividedBy180;
        }
    }
}

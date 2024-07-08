namespace Airport_Distance.Calculators
{
    public interface IDistanceCalculator
    {
        double CalculateDistance(double latitude1, double longitude1, double latitude2, double longitude2);
    }
}

using System;

namespace Airport_Distance.Calculators
{
    public class HaversineDistanceCalculator : IDistanceCalculator
    {
        private const double EarthRadiusInMeters = 6378160;

        //https://stackoverflow.com/questions/365826/calculate-distance-between-2-gps-coordinates - формула Хаверсина для вычисления расстояния на сфере между двумя точками
        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2) // Возвращает результат в метрах
        {
            var dLat = (lat2 - lat1) * Math.PI / 180.0;
            var dLon = (lon2 - lon1) * Math.PI / 180.0;

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(lat1 * Math.PI / 180.0) *
                Math.Cos(lat2 * Math.PI / 180.0) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusInMeters * c;
        }
    }

}

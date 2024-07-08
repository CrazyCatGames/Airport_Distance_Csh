using Newtonsoft.Json;

namespace Airport_Distance.Models
{
    public class Airport
    {
        public string Iata { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Location Location { get; set; }
    }
    public class Location
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}

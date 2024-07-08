using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Airport_Distance.Models;
using System.Linq;

namespace Airport_Distance.Services
{
    public class AirportService : IAirportService
    {
        private readonly RestClient _client;
        private const string Base_URL = "https://places-dev.cteleport.com";

        public AirportService()
        {
            _client = new RestClient(Base_URL);
        }

        public async Task<Airport> GetAirportAsync(string iataCode)
        {
            ValidateAirportIataCode(iataCode);

            var request = new RestRequest($"airports/{iataCode.ToUpper()}", Method.GET);
            var response = await _client.ExecuteAsync<Airport>(request);
            ValidateResponse(response, iataCode);

            return JsonConvert.DeserializeObject<Airport>(response.Content);
        }

        private static void ValidateAirportIataCode(string iataCode)
        {
            if (string.IsNullOrWhiteSpace(iataCode) || (iataCode.Length != 3) || (!iataCode.All(char.IsLetter))) //проверить на длину
            {
                throw new ArgumentException("IATA code must be provided", nameof(iataCode));
            }
        }

        private static void ValidateResponse(IRestResponse response, string iataCode)
        {
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.NotFound:
                    throw new Exception($"Airport with IATA code '{iataCode}' not found.");

                case System.Net.HttpStatusCode.BadRequest:
                    throw new Exception("Bad request. Please check the IATA code provided.");

                case System.Net.HttpStatusCode.InternalServerError:
                    throw new Exception("Internal server error. Please try again later.");

                default:
                    if (!response.IsSuccessful)
                    {
                        throw new Exception("Unable to fetch airport data. Status code: " + response.StatusCode);
                    }
                    break;
            }
        }
    }
}

using System.Threading.Tasks;
using Airport_Distance.Models;

namespace Airport_Distance.Services
{
    public interface IAirportService
    {
        Task<Airport> GetAirportAsync(string iataCode);
    }
}

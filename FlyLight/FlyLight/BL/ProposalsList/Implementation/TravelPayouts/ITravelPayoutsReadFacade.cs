using System.Threading.Tasks;
using FlyLight.BL.ProposalsList.DTO;
using Newtonsoft.Json.Linq;

namespace FlyLight.BL.ProposalsList.Implementation.TravelPayouts
{
    public interface ITravelPayoutsReadFacade
    {
        Task<JObject> SearchFlights(ProposalsListFilterWrapper filter);
        Task<JArray> SearchFlightResult(string searchId);
    }
}
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using FlyLight.BL.ProposalsList.DTO;
using Newtonsoft.Json.Linq;

namespace FlyLight.BL.ProposalsList.Implementation.TravelPayouts.Fake
{
    public class FakeTravelPayoutsReadFacade : ITravelPayoutsReadFacade
    {
        public async Task<JObject> SearchFlights(ProposalsListFilterWrapper filter)
        {
            var assembly = typeof(FakeTravelPayoutsReadFacade).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("FlyLight.BL.ProposalsList.Implementation.TravelPayouts.Fake.response1.json");
            return JObject.Parse(new StreamReader(stream).ReadToEnd());
        }

        public async Task<JArray> SearchFlightResult(string searchId)
        {
            var assembly = typeof(FakeTravelPayoutsReadFacade).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("FlyLight.BL.ProposalsList.Implementation.TravelPayouts.Fake.response2.json");
            return JArray.Parse(new StreamReader(stream).ReadToEnd());
        }
    }
}

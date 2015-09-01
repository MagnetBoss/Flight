using System;
using System.IO;
using System.Threading.Tasks;
using FlyLight.BL.ProposalsList.DTO;
using FlyLight.BL.ProposalsList.Implementation.TravelPayouts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Map_From_Json_Generate_Right_Dto()
        {
            var file = File.ReadAllText("response.json");
            var dto = TravelPayoutsMapper.MapFromAgentJson(JArray.Parse(file)[0]);
            
            Assert.IsNotNull(dto);
        }

        [TestMethod]
        public void Generate_Valid_OneWay_Filter_Request()
        {
            var json = TravelPayoutsTravelPayoutsReadFacade.GenerateRequestJson(new ProposalsListFilterWrapper
            {
                Adults = 1,
                Children = 0,
                Infants = 0,
                DepatureCityIata = "MOW",
                DepatureDate = DateTime.Now,
                ArrivalCityIata = "LED"
            });

            Assert.IsNotNull(json);
        }


        public class MockTravelPayoutsReadFacade : ITravelPayoutsReadFacade
        {
            public async Task<JObject> SearchFlights(ProposalsListFilterWrapper filter)
            {
                var result = new JObject();
                result["search_id"] = "3c406327-4107-44fb-9ee4-833f688dfd37";
                return result;
            }

            public async Task<JArray> SearchFlightResult(string searchId)
            {
                return JArray.Parse(File.ReadAllText("real_response_2.json"));
            }
        }

        [TestMethod]
        public void Request_Flights_From_TravelPayouts()
        {
            var proposalsService = new TravelPayoutsProposalsListService(new MockTravelPayoutsReadFacade());
            var task = proposalsService.GetProposalsListAsync(new ProposalsListFilterWrapper
            {
                Adults = 1,
                Children = 0,
                Infants = 0,
                DepatureCityIata = "DME",
                DepatureDate = DateTime.Now.AddDays(5),
                ArrivalCityIata = "LED"
            });
            var proposals = task.Result;
        }

        [TestMethod]
        public void Generate_Valid_Signature_From_TravelPayots_Demo()
        {
            var file = File.ReadAllText("request.json");
            var requestJson = JObject.Parse(file);
            var signature = RequestSignatureGenerator.GenerateSignature(requestJson, "08fe1910088a2a5638939991eaf4a59e");
            Assert.AreEqual("6f2d3d52db19a0537791a33a030260de", signature);
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FlyLight.BL.ProposalsList.DTO;
using FlyLight.BL.ProposalsList.Implementation.TravelPayouts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace Tests
{
    [TestClass]
    public class TravelPayoutsTests
    {
        [TestMethod]
        public void Map_From_Json_Generate_Right_Dto()
        {
            var file = File.ReadAllText("response.json");
            var dto = TravelPayoutsMapper.MapFromAgentJson(JArray.Parse(file)[0] as JObject);
            Assert.IsNotNull(dto);
            Assert.IsNotNull(dto.FirstOrDefault());
            Assert.IsNotNull(dto.First().Currency);
            Assert.IsNotNull(dto.First().ForwardTicketSegment);
            Assert.IsNotNull(dto.First().ForwardTicketSegment.ArrivalAirport);
            Assert.IsNotNull(dto.First().ForwardTicketSegment.DepartureAirport);
            Assert.IsNotNull(dto.First().ForwardTicketSegment.LocalArrivalTime);
            Assert.IsNotNull(dto.First().ForwardTicketSegment.LocalDepatureTime);
            Assert.IsNotNull(dto.First().Price);
            Assert.IsNotNull(dto.First().ReturnTicketSegment);
            Assert.IsNotNull(dto.First().ReturnTicketSegment.ArrivalAirport);
            Assert.IsNotNull(dto.First().ReturnTicketSegment.DepartureAirport);
            Assert.IsNotNull(dto.First().ReturnTicketSegment.LocalArrivalTime);
            Assert.IsNotNull(dto.First().ReturnTicketSegment.LocalDepatureTime);
            Assert.IsNotNull(dto.First().TicketSignId);
            Assert.IsNotNull(dto.First().ValidatingCarrierIconUrl);
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
                return JObject.Parse(File.ReadAllText("real_response_1.json"));
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
            Assert.IsNotNull(proposals);
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

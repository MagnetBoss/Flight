using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FlyLight.BL.ProposalsList.DTO;
using FlyLight.Infrastructure;
using Newtonsoft.Json.Linq;

namespace FlyLight.BL.ProposalsList.Implementation.TravelPayouts
{
    public class TravelPayoutsTravelPayoutsReadFacade : ITravelPayoutsReadFacade
    {
        public async Task<JObject> SearchFlights(ProposalsListFilterWrapper filter)
        {
            const string url = "http://api.travelpayouts.com/v1/flight_search";

            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "POST";
            var requestJson = GenerateRequestJson(filter);
            var signature = RequestSignatureGenerator.GenerateSignature(requestJson, "08fe1910088a2a5638939991eaf4a59e");
            requestJson.Add("signature", signature);
            var postStream =
                await Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, null);
            using (var writer = new StreamWriter(postStream))
            {
                writer.Write(requestJson.ToString());
                writer.Flush();
                writer.Dispose();
            }

            var response =
                await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null);

            string result;

            using (var stream = response.GetResponseStream())
            {
                result = new StreamReader(stream).ReadToEnd();
            }

            dynamic v = JObject.Parse(result);
            return v;
        }

        public async Task<JArray> SearchFlightResult(string searchId)
        {
            const string url = "http://api.travelpayouts.com/v1/flight_search_results";

            var request = (HttpWebRequest)WebRequest.Create(new Uri(url + "?uuid=" + searchId));
            request.ContentType = "application/json";
            request.Headers["Accept-Encoding"] = "gzip";
            request.Method = "GET";

            var response =
                await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null);

            string result;

            using (var stream = response.GetDecompressedStream())
            {
                result = new StreamReader(stream).ReadToEnd();
            }

            return JArray.Parse(result);
        }


        public static JObject GenerateRequestJson(ProposalsListFilterWrapper filter)
        {
            dynamic request = new
            {
                host = "beta.aviasales.ru",
                marker = "78042",
                user_ip = "127.0.0.1",
                locale = "ru",
                trip_class = "Y",
                passengers = new
                {
                    adults = filter.Adults,
                    children = filter.Children,
                    infants = filter.Infants,
                },
                segments = new[]
                {
                    new
                    {
                        origin = filter.DepatureCityIata,
                        destination = filter.ArrivalCityIata,
                        date = filter.DepatureDate.ToString("yyyy-MM-dd"),
                    }
                }

            };

            return JObject.FromObject(request);
        }
    }
}

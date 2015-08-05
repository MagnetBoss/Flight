using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FlyLight.Model.TicketsSearch.DTO;
using FlyLight.Model.TicketsSearch.Interfaces;
using FlyLight.Tools;
using Newtonsoft.Json;

namespace FlyLight.Model.TicketsSearch.Implementation.Real
{
    public class TicketsSearchService : ITicketsSearchService
    {
        public async Task<IList<ProposalDto>> RoundTripTicketsAsync(int adults, int children, int infants, string origin,
            string destination, DateTime timeTo,
            DateTime timeFrom)
        {
            const string url = "http://api.travelpayouts.com/v1/flight_search";

            var request = (HttpWebRequest) WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            var response =
                await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null);

            using (var stream = response.GetDecompressedStream())
            {
                return JsonConvert.DeserializeObject<IList<ProposalDto>>(new StreamReader(stream).ReadToEnd());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using FlyLight.BL.ProposalsList.DTO;
using FlyLight.BL.ProposalsList.Interfaces;
using System.Threading.Tasks;
using FlyLight.Model.TicketsSearch.DTO;
using FlyLight.Tools;
using Newtonsoft.Json;

namespace FlyLight.BL.ProposalsList.Implementation
{
    public class ProposalsListService : IProposalsListService
    {
        public async Task<IList<ProposalOverviewDto>> GetProposalsListAsync(ProposalsListFilterWrapper filter)
        {
            const string url = "http://api.travelpayouts.com/v1/flight_search";

            var request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            var response =
                await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null);

            string result;

            using (var stream = response.GetDecompressedStream())
            {
                result = new StreamReader(stream).ReadToEnd();
            }

            return null;
        }
    }
}

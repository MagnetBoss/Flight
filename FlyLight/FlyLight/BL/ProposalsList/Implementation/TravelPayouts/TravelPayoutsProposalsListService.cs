using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using FlyLight.BL.ProposalsList.DTO;
using FlyLight.BL.ProposalsList.Interfaces;
using Newtonsoft.Json.Linq;

namespace FlyLight.BL.ProposalsList.Implementation.TravelPayouts
{
    public class TravelPayoutsProposalsListService : IProposalsListService
    {
        private readonly ITravelPayoutsReadFacade _travelPayoutsReadFacade;

        public TravelPayoutsProposalsListService(ITravelPayoutsReadFacade travelPayoutsReadFacade)
        {
            _travelPayoutsReadFacade = travelPayoutsReadFacade;
        }

        public async Task<IList<ProposalOverviewDto>> GetProposalsListAsync(ProposalsListFilterWrapper filter)
        {
            var json = await _travelPayoutsReadFacade.SearchFlights(filter);
            string searchId = json["search_id"].Value<string>();

            bool lastAgentReached = false;

            var proposalsJson = new List<JObject>();

            while (!lastAgentReached)
            {
                var agentResponseJson = await _travelPayoutsReadFacade.SearchFlightResult(searchId);
                if (agentResponseJson.Count == 0)
                {
                    break;
                }

                var lastProposal = agentResponseJson.Last as JObject;
                if (lastProposal == null)
                {
                    break;
                }

                lastAgentReached = lastProposal.Properties().Count() == 1 &&
                                   lastProposal.Properties().First().Name == "search_id";

                proposalsJson.AddRange(agentResponseJson.OfType<JObject>());
            }
            if (lastAgentReached)
                proposalsJson.RemoveAt(proposalsJson.Count - 1); //удаляем последний фиктивный "proposal"

            var proposalsResult = new List<ProposalOverviewDto>();

            foreach (var proposal in proposalsJson)
            {
                proposalsResult.AddRange(TravelPayoutsMapper.MapFromAgentJson(proposal));
            }

            return proposalsResult;
        }

    }
    
}

using System;
using System.Collections.Generic;
using FlyLight.BL.ProposalsList.DTO;
using FlyLight.BL.ProposalsList.Interfaces;
using System.Threading.Tasks;

namespace FlyLight.BL.ProposalsList.Implementation
{
    public class ProposalsListService : IProposalsListService
    {
        public async Task<IList<ProposalOverviewDto>> GetProposalsListAsync(ProposalsListFilterWrapper filter)
        {
            return null;
        }
    }
}

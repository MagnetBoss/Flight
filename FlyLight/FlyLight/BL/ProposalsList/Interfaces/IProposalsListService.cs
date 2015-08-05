using FlyLight.BL.ProposalsList.DTO;
using FlyLight.BL.ProposalsList.Implementation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlyLight.BL.ProposalsList.Interfaces
{
    public interface IProposalsListService
    {
        Task<IList<ProposalOverviewDto>> GetProposalsListAsync(ProposalsListFilterWrapper filter); 
    }
}

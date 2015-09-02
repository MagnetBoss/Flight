using FlyLight.BL.ProposalsList.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlyLight.BL.ProposalsList.Interfaces
{
    public interface IProposalsListService
    {
        Task<IList<ProposalOverviewDto>> GetProposalsListAsync(ProposalsListFilterWrapper filter);
    }
}

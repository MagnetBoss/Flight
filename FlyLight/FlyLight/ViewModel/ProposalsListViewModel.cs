using FlyLight.BL.ProposalsList.Interfaces;
using ReactiveUI;
using GalaSoft.MvvmLight.Messaging;
using FlyLight.ViewModel.Messaging;
using System.Threading.Tasks;
using FlyLight.BL.ProposalsList.DTO;
using System.Collections.Generic;
using System.Linq;

namespace FlyLight.ViewModel
{
    public class ProposalsListViewModel : ReactiveObject
    {
        public IProposalsListService ProposalsListService { get; private set; }

        public ProposalsListViewModel(IProposalsListService proposalsListService)
        {
            ProposalsListService = proposalsListService;

            Proposals = new List<ProposalOverviewDto>();

            Messenger.Default.Register<ShowProposalsListMessage>(this, async message =>
            {
                await FetchProposals(message.Filter);
            });
        }

        public async Task FetchProposals(ProposalsListFilterWrapper filter)
        {
            const int maxProposalsCount = 75;
            var proposals = await ProposalsListService.GetProposalsListAsync(filter);
            Proposals = proposals.OrderBy(p => p.Price).Take(maxProposalsCount).ToList();
        }

        private List<ProposalOverviewDto> _proposals;
        public List<ProposalOverviewDto> Proposals
        {
            get { return _proposals; }
            private set { this.RaiseAndSetIfChanged(ref _proposals, value); }
        }

    }
}

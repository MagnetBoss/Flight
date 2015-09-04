using FlyLight.BL.ProposalsList.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using FlyLight.ViewModel.Messaging;
using System.Threading.Tasks;
using FlyLight.BL.ProposalsList.DTO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using FlyLight.BL.ProposalsList.Implementation.TravelPayouts;
using FlyLight.BL.ProposalsList.Implementation.TravelPayouts.Fake;

namespace FlyLight.ViewModel
{
    public class ProposalsListViewModel : MvxViewModel
    {
        public IProposalsListService ProposalsListService { get; private set; }

        public ProposalsListViewModel(IProposalsListService proposalsListService)
        {
            ProposalsListService = proposalsListService;

            Proposals = new ObservableCollection<ProposalOverviewDto>();

            FetchProposals(null).Wait();

            Messenger.Default.Register<ShowProposalsListMessage>(this, async message =>
            {
                await FetchProposals(message.Filter);
            });
        }

        //Only for design mode
        public ProposalsListViewModel() : this(new TravelPayoutsProposalsListService(new FakeTravelPayoutsReadFacade()))
        {
            
        }

        public async Task FetchProposals(ProposalsListFilterWrapper filter)
        {
            const int maxProposalsCount = 75;
            var proposals = await ProposalsListService.GetProposalsListAsync(filter);
            Proposals = new ObservableCollection<ProposalOverviewDto>(proposals.OrderBy(p => p.Price).Take(maxProposalsCount).ToList());
        }

        private ObservableCollection<ProposalOverviewDto> _proposals;
        public ObservableCollection<ProposalOverviewDto> Proposals
        {
            get { return _proposals; }
            private set
            {
                if (value == _proposals) return;
                _proposals = value;
                RaisePropertyChanged(() => Proposals);
            }
        }
    }
}

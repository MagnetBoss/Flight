using FlyLight.BL.ProposalsList.Implementation.TravelPayouts;
using FlyLight.BL.ProposalsList.Implementation.TravelPayouts.Fake;
using FlyLight.BL.ProposalsList.Interfaces;
using FlyLight.Model.TicketsSearch.Implementation.Stub;
using FlyLight.Model.TicketsSearch.Interfaces;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace FlyLight.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IPlacesAutoCompleteService, FakePlacesAutoCompleteService>();
            SimpleIoc.Default.Register<IProposalsListService, TravelPayoutsProposalsListService>();
            SimpleIoc.Default.Register<ITravelPayoutsReadFacade, FakeTravelPayoutsReadFacade>();

            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<ProposalsListViewModel>();
        }

        public MainPageViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }
        }

        public ProposalsListViewModel ProposalsList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProposalsListViewModel>();
            }
        }
    }
}
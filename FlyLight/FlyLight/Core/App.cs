using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using FlyLight.BL.ProposalsList.Implementation.TravelPayouts;
using FlyLight.BL.ProposalsList.Implementation.TravelPayouts.Fake;
using FlyLight.BL.ProposalsList.Interfaces;
using FlyLight.Model.TicketsSearch.Implementation.Stub;
using FlyLight.Model.TicketsSearch.Interfaces;

namespace FlyLight.Core
{
    public class App : MvxApplication
    {
        public App()
        {
            Mvx.RegisterType<IPlacesAutoCompleteService, FakePlacesAutoCompleteService>();
            Mvx.RegisterType<IProposalsListService, TravelPayoutsProposalsListService>();
            Mvx.RegisterType<ITravelPayoutsReadFacade, FakeTravelPayoutsReadFacade>();
        }
    }
}

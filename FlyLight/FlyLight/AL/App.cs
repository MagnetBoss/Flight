using System;
using System.Linq;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using FlyLight.BL.CitiesAutoComplete.Implementation;
using FlyLight.BL.CitiesAutoComplete.Interface;
using FlyLight.BL.ProposalsList.Implementation.TravelPayouts;
using FlyLight.BL.ProposalsList.Implementation.TravelPayouts.Fake;
using FlyLight.BL.ProposalsList.Interfaces;
using FlyLight.ViewModel;

namespace FlyLight.AL
{
    public class App : MvxApplication
    {
        public App()
        {
            Mvx.RegisterType<ICitiesAutoCompleteService, CitiesAutoCompleteService>();
            Mvx.RegisterType<ICitiesAutoCompleteReadFacade, CitiesAutoCompleteReadFacade>();
            Mvx.RegisterType<IProposalsListService, TravelPayoutsProposalsListService>();
            Mvx.RegisterType<ITravelPayoutsReadFacade, FakeTravelPayoutsReadFacade>();

            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainPageViewModel>());
        }

        public Type FindViewModelTypeByName(string typeName)
        {
            return CreatableTypes().FirstOrDefault(t => t.Name == typeName);
        }
    }
}

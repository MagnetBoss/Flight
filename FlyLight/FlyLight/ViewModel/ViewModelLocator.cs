using FlyLight.Model.TicketsSearch.DesignMode;
using FlyLight.Model.TicketsSearch.Implementation.Stub;
using FlyLight.Model.TicketsSearch.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace FlyLight.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<ITicketsSearchService, TicketSearchServiceDesign>();
                SimpleIoc.Default.Register<IPlacesAutoCompleteService, FakePlacesAutoCompleteService>();
            }
            else
            {
                SimpleIoc.Default.Register<ITicketsSearchService, TicketSearchServiceDesign>();
                SimpleIoc.Default.Register<IPlacesAutoCompleteService, FakePlacesAutoCompleteService>();
            }

            SimpleIoc.Default.Register<MainPageViewModel>();
        }

        public MainPageViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }
        }
    }
}
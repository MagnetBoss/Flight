using Cirrious.MvvmCross.ViewModels;
using FlyLight.BL.CitiesAutoComplete.Interface;
using ReactiveUI;

namespace FlyLight.ViewModel
{
    public class CitySearchViewModel : MvxViewModel
    {
        private readonly ICitiesAutoCompleteService _citiesAutoCompleteService;

        public CitySearchViewModel(ICitiesAutoCompleteService cityAutoCompleteService)
        {
            _citiesAutoCompleteService = cityAutoCompleteService;

            _citiesAutoCompleteService.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "CitiesOptions")
                    RaisePropertyChanged(() => CitiesOptions);
            };
        }

        public string City
        {
            get { return _citiesAutoCompleteService.City; }
            set
            {
                _citiesAutoCompleteService.City = value;
            }
        }

        public IReactiveList<string> CitiesOptions
        {
            get { return _citiesAutoCompleteService.CitiesOptions; }
        }
    }
}

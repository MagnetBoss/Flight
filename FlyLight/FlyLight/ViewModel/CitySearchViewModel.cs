using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using FlyLight.Model.TicketsSearch.Interfaces;
using ReactiveUI;

namespace FlyLight.ViewModel
{
    public class CitySearchViewModel : ReactiveObject
    {
        public CitySearchViewModel(IPlacesAutoCompleteService placesAutoCompleteService)
        {
            CitiesOptions = new ReactiveList<string>();

            SearchCityCommand = ReactiveCommand.CreateAsyncTask(
                this.WhenAny(x => x.City, x => x.CitiesOptions,
                    (city, options) => CanSearchForCities(city.Value, options.Value)),
                async _ =>
                    await placesAutoCompleteService.GetPlaces(City, "en")
                );

            this.WhenAnyValue(x => x.City, x => x.CitiesOptions)
                .Select(x => x.Item2.Count > 0 && CanSearchForCities(x.Item1, x.Item2))
                .ToProperty(this, x => x.ShowOptions, out _showOptions);


            SearchCityCommand.Subscribe(results =>
            {
                var newCitiesOptions = new ReactiveList<string>();
                foreach (var result in results.Take(7))
                {
                    newCitiesOptions.Add(result);
                }
                CitiesOptions = newCitiesOptions;
            });

            SearchCityCommand.ThrownExceptions
                .Subscribe(ex => UserError.Throw("Potential Network Connectivity Error", ex));

            this.WhenAnyValue(x => x.City).Throttle(TimeSpan.FromSeconds(2.5), RxApp.MainThreadScheduler)
                .InvokeCommand(this, x => x.SearchCityCommand);
        }

        private bool CanSearchForCities(string city, IEnumerable<string> citiesOptions)
        {
            var q = !string.IsNullOrWhiteSpace(city) && !citiesOptions.Contains(city);
            return q;
        }

        private readonly ObservableAsPropertyHelper<bool> _showOptions;
        public bool ShowOptions
        {
            get { return _showOptions.Value; }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    CitiesOptions.Clear();
                }
                this.RaiseAndSetIfChanged(ref _city, value);
            }
        }

        private ReactiveList<string> _citiesOptions;
        public ReactiveList<string> CitiesOptions
        {
            get { return _citiesOptions; }
            private set { this.RaiseAndSetIfChanged(ref _citiesOptions, value); }
        }

        public ReactiveCommand<List<string>> SearchCityCommand { get; set; }
    }
}

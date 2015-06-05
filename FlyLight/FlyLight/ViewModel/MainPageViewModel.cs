using System;
using System.Windows.Input;
using FlyLight.Model.TicketsSearch;
using FlyLight.Model.TicketsSearch.Interfaces;
using GalaSoft.MvvmLight.Command;
using ReactiveUI;

namespace FlyLight.ViewModel
{
    public class MainPageViewModel : ReactiveObject
    {
        public MainPageViewModel(IPlacesAutoCompleteService placesAutoCompleteService)
        {
            DepatureCityViewModel = new CitySearchViewModel(placesAutoCompleteService);
            ArrivalCityViewModel = new CitySearchViewModel(placesAutoCompleteService);
            DepatureDate = DateTime.Now;
            ReturnDate = DepatureDate.AddDays(2);
            AdultsCount = 1;
        }

        public CitySearchViewModel DepatureCityViewModel { get; private set; }
        public CitySearchViewModel ArrivalCityViewModel { get; private set; }

        private DateTime _depatureDate;
        public DateTime DepatureDate
        {
            get { return _depatureDate; }
            set
            {
                this.RaiseAndSetIfChanged(ref _depatureDate, value);
            }
        }
        
        private DateTime _returnDate;
        public DateTime ReturnDate
        {
            get { return _returnDate; }
            set
            {
                this.RaiseAndSetIfChanged(ref _returnDate, value);
            }
        }

        private bool _returnTicketEnabled;
        public bool ReturnTicketEnabled
        {
            get { return _returnTicketEnabled; }
            set { this.RaiseAndSetIfChanged(ref _returnTicketEnabled, value); }
        }

        private int _adultsCount;
        public int AdultsCount
        {
            get { return _adultsCount; }
            set { this.RaiseAndSetIfChanged(ref _adultsCount, value); }
        }

        private int _childrenCount;
        public int ChildrenCount //дети 2-12 лет
        {
            get { return _childrenCount; }
            set { this.RaiseAndSetIfChanged(ref _childrenCount, value); }
        }

        private int _babbiesCount;
        public int BabbiesCount //дети 0-2 года
        {
            get { return _babbiesCount; }
            set { this.RaiseAndSetIfChanged(ref _babbiesCount, value); }
        }
        public ICommand SearchTicketsCommand
        {
            get
            {
                return new RelayCommand(() =>
                {

                });
            }
        }
    }
}

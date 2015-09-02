using System;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using FlyLight.BL.ProposalsList.DTO;
using FlyLight.Model.TicketsSearch.Interfaces;
using FlyLight.ViewModel.Messaging;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FlyLight.ViewModel
{
    public class MainPageViewModel : MvxViewModel
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
                if (value == _depatureDate) return;
                RaisePropertyChanged(() => DepatureDate);
            }
        }
        
        private DateTime _returnDate;
        public DateTime ReturnDate
        {
            get { return _returnDate; }
            set
            {
                if (value == _returnDate) return;
                RaisePropertyChanged(() => ReturnDate);
            }
        }

        private bool _returnTicketEnabled;
        public bool ReturnTicketEnabled
        {
            get { return _returnTicketEnabled; }
            set
            {
                if (value == _returnTicketEnabled) return;
                RaisePropertyChanged(() => ReturnTicketEnabled);
            }
        }

        private int _adultsCount;
        public int AdultsCount
        {
            get { return _adultsCount; }
            set
            {
                if (value == _adultsCount) return;
                RaisePropertyChanged(() => AdultsCount);
            }
        }

        private int _childrenCount;
        public int ChildrenCount //дети 2-12 лет
        {
            get { return _childrenCount; }
            set
            {
                if (value == _childrenCount) return;
                RaisePropertyChanged(() => ChildrenCount);
            }
        }

        private int _babbiesCount;
        public int BabbiesCount //дети 0-2 года
        {
            get { return _babbiesCount; }
            set
            {
                if (value == _babbiesCount) return;
                RaisePropertyChanged(() => BabbiesCount);
            }
        }
        public ICommand SearchTicketsCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Messenger.Default.Send(new ShowProposalsListMessage
                    {
                        Filter = new ProposalsListFilterWrapper
                        {
                            Children = ChildrenCount,
                            Adults = AdultsCount,
                            Infants = BabbiesCount,
                            DepatureCityIata = DepatureCityViewModel.City,
                            DepatureDate = DepatureDate,
                            ArrivalCityIata = ArrivalCityViewModel.City
                        }
                    });
                });
            }
        }
    }
}
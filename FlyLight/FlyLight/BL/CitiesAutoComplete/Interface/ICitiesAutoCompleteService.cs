using System.Collections.Generic;
using System.ComponentModel;
using ReactiveUI;

namespace FlyLight.BL.CitiesAutoComplete.Interface
{
    public interface ICitiesAutoCompleteService : INotifyPropertyChanged
    {
        bool ShowOptions { get; }
        string City { get; set; }
        IReactiveList<string> CitiesOptions { get; }
    }
}

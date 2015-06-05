using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlyLight.Model.TicketsSearch.Interfaces
{
    public interface IPlacesAutoCompleteService
    {
        Task<List<string>> GetPlaces(string term, string locale);
    }
}

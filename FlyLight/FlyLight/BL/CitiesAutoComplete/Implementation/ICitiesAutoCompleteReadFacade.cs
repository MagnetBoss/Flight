using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlyLight.BL.CitiesAutoComplete.Implementation
{
    public interface ICitiesAutoCompleteReadFacade
    {
        Task<List<string>> GetCities(string term, string locale);
    }
}

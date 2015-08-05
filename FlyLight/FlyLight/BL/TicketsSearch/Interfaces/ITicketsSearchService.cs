using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlyLight.Model.TicketsSearch.DTO;

namespace FlyLight.Model.TicketsSearch.Interfaces
{
    public interface ITicketsSearchService
    {
        Task<IList<ProposalDto>> RoundTripTicketsAsync(int adults, int children, int infants, string origin, string destination, DateTime timeTo, DateTime timeFrom);
    }
}

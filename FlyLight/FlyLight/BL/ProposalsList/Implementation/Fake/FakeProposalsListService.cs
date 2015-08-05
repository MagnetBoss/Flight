using System.Collections.Generic;
using FlyLight.BL.ProposalsList.DTO;
using FlyLight.BL.ProposalsList.Interfaces;
using System;
using System.Threading.Tasks;

namespace FlyLight.BL.ProposalsList.Implementation.DesignMode
{
    public class FakeProposalsListService : IProposalsListService
    {
        public async Task<IList<ProposalOverviewDto>> GetProposalsListAsync(ProposalsListFilterWrapper filter)
        {
            return new List<ProposalOverviewDto>
            {
                new ProposalOverviewDto
                {
                    Currency = "RUB",
                    Price = new decimal(4092.12),
                    TicketSignId = "1",
                    ValidatingCarrierIconUrl = "http://pics.avs.io/120/40/SU.png",
                    Segments = new List<SegmentOverviewDto>
                    {
                        //Туда
                        new SegmentOverviewDto
                        {
                            DepartureAirport = "VKO",
                            ArrivalAirport = "LED",
                            LocalDepatureTime = DateTime.Now,
                            LocalArrivalTime = DateTime.Now.AddHours(1.5)
                        },
                        //Обратно через 2 дня
                        new SegmentOverviewDto
                        {
                            DepartureAirport = "LED",
                            ArrivalAirport = "SWO",
                            LocalDepatureTime = DateTime.Now.AddDays(2),
                            LocalArrivalTime = DateTime.Now.AddDays(2).AddHours(1.5)
                        }
                    }
                }
            };
        }
    }
}

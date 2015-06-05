using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlyLight.Model.TicketsSearch.DTO;
using FlyLight.Model.TicketsSearch.Interfaces;

namespace FlyLight.Model.TicketsSearch.DesignMode
{
    public class TicketSearchServiceDesign : ITicketsSearchService
    {
        public async Task<IList<ProposalDto>> RoundTripTicketsAsync(int adults, int children, int infants, string origin, string destination, DateTime timeTo,
            DateTime timeFrom)
        {
            return new List<ProposalDto>
            {
                new ProposalDto
                {
                    Currency = "RUB",
                    Price = 1234,
                    Segments = new List<SegmentDto>
                    {
                        new SegmentDto
                        {
                            Flights = new List<FlightDto>
                            {
                                new FlightDto
                                {
                                    ArrivalAirport = "SWO",
                                    ArrivalPosixTime = 1232142,
                                    DepartureAirport = "LED",
                                    DeparturePosixTime = 1231243,
                                    FlightDuration = TimeSpan.FromMinutes(90),
                                    FlightNumber = "F123",
                                    OperatingCarrier = "AS"
                                }
                            }
                        }
                    }
                },
                new ProposalDto
                {
                    Currency = "RUB",
                    Price = 2634,
                    Segments = new List<SegmentDto>
                    {
                        new SegmentDto
                        {
                            Flights = new List<FlightDto>
                            {
                                new FlightDto
                                {
                                    ArrivalAirport = "SWO",
                                    ArrivalPosixTime = 1232142,
                                    DepartureAirport = "LED",
                                    DeparturePosixTime = 1231243,
                                    FlightDuration = TimeSpan.FromMinutes(90),
                                    FlightNumber = "F123",
                                    OperatingCarrier = "AS"
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}

using System;

namespace FlyLight.Model.TicketsSearch.DTO
{
    public class FlightDto
    {
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public long DeparturePosixTime { get; set; }
        public long ArrivalPosixTime { get; set; }
        public TimeSpan FlightDuration { get; set; }
        public string FlightNumber { get; set; }
        public string OperatingCarrier { get; set; }
    }
}

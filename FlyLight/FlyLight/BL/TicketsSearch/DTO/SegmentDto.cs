using System;

namespace FlyLight.Model.TicketsSearch.DTO
{
    public class SegmentDto
    {
        //IATA код аэропорта отправления, e.g. SWO
        public string DepartureAirport { get; set; } 

        //IATA код аэропорта прибытия, e.g. SWO
        public string ArrivalAirport { get; set; }

        //Местное время отправления
        public DateTime LocalDepatureTime { get; set; }

        //Местное время прибытия
        public DateTime LocalArrivalTime { get; set; }

        public TimeSpan FlightDuration { get; set; }

        //Номер рейса
        public string FlightNumber { get; set; }
        
        //IATA код авиакомпании, выполняющей перевозку
        public string OperatingCarrier { get; set; } 
    }
}

using System;

namespace FlyLight.BL.ProposalsList.DTO
{
    //Один SegmentOverviewDto - один перелет в конкретном предложении в общем списке
    public class SegmentOverviewDto
    {
        //IATA код аэропорта отправления, e.g. SWO
        public string DepartureAirport { get; set; } 

        //IATA код аэропорта прибытия, e.g. SWO
        public string ArrivalAirport { get; set; }

        //Местное время отправления
        public DateTime LocalDepatureTime { get; set; }

        //Местное время прибытия
        public DateTime LocalArrivalTime { get; set; }
    }
}

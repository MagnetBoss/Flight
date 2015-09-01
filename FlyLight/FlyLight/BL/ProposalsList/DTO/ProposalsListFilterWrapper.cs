using System;

namespace FlyLight.BL.ProposalsList.DTO
{
    public class ProposalsListFilterWrapper
    {
        public string DepatureCityIata { get; set; }
        public string ArrivalCityIata { get; set; }
        public DateTime DepatureDate { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Infants { get; set; }

    }
}

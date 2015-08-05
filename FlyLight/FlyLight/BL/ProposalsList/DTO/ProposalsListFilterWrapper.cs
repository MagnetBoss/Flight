using System;

namespace FlyLight.BL.ProposalsList.DTO
{
    public class ProposalsListFilterWrapper
    {
        public string DepatureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepatureDate { get; set; }
    }
}

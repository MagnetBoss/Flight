using System.Collections.Generic;

namespace FlyLight.Model.TicketsSearch.DTO
{
    public class ProposalDto
    {
        public IList<SegmentDto> Segments { get; set; }
        public int Price { get; set; }
        public string Currency { get; set; }
    }
}

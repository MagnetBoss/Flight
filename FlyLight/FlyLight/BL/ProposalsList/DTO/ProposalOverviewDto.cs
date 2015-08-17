using System.Collections.Generic;

namespace FlyLight.BL.ProposalsList.DTO
{
    //Один ProposalOverviewDto - один предложение в общем списке предложений
    public class ProposalOverviewDto
    {
        //В каждом сегменте перелета - инфа о перелете из одного аэропорта в другой
        public SegmentOverviewDto ForwardTicketSegment { get; set; } //Перелет "ТУДА" 
        public SegmentOverviewDto ReturnTicketSegment { get; set; } //Перелет "ОБРАТНО"
         
        public decimal Price { get; set; }
        
        //Валюта, в которой указана цена
        public string Currency { get; set; }

        //Уникальный id билета, для объединения информации от разных агентств в один билет;
        public string TicketSignId { get; set; }

        //Путь к логотипу авиакомпании 
        //Пример: http://pics.avs.io/120/80/SU.png (120 - ширина, 80 - высота, "SU" - IATA код основной авиакомпании)
        //IATA код основной авиакомпании (в каждом сегменте указан также IATA код компании, выполняющей перевозку)
        //т.е. основная компания может быть одна, а выполняющая перевозку - другая 
        public string ValidatingCarrierIconUrl { get; set; }
    }
}
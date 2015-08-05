using System.Collections.Generic;

namespace FlyLight.Model.TicketsSearch.DTO
{
    public class ProposalDto
    {
        //В каждом сегменте перелета - инфа о перелете из одного аэропорта в другой
        public IList<SegmentDto> Segments { get; set; }
        
        public decimal Price { get; set; }
        
        //Валюта, в которой указана цена
        public string Currency { get; set; }

        //Компания, продающая билеты
        public SellCompanyDto SellCompany { get; set; }

        //Уникальный id билета, для объединения информации от разных агентств в один билет;
        public string TicketSignId { get; set; }

        //IATA код основной авиакомпании (в каждом сегменте указан также IATA код компании, выполняющей перевозку)
        //т.е. основная компания может быть одна, а выполняющая перевозку - другая 
        //IATA - International Air Transport Association
        public string ValidatingCarrier { get; set; }
    }
}
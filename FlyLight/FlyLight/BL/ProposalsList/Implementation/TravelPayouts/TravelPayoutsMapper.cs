using System;
using System.Collections.Generic;
using System.Linq;
using FlyLight.BL.ProposalsList.DTO;
using FlyLight.Tools.Date;
using Newtonsoft.Json.Linq;

namespace FlyLight.BL.ProposalsList.Implementation.TravelPayouts
{
    public class TravelPayoutsMapper
    {
        private static SegmentOverviewDto MapSegmentFromJson(dynamic flightInfo)
        {
            long depatureTimestamp = flightInfo.local_departure_timestamp;
            long arrivalTimestamp = flightInfo.local_arrival_timestamp;
            var segmentDto = new SegmentOverviewDto
            {
                DepartureAirport = flightInfo.departure,
                LocalDepatureTime = UnixTimeToDateTime.ConvertUnixToDateTime(depatureTimestamp),
                ArrivalAirport = flightInfo.arrival,
                LocalArrivalTime = UnixTimeToDateTime.ConvertUnixToDateTime(arrivalTimestamp)
            };

            return segmentDto;
        }

        public static IList<ProposalOverviewDto> MapFromAgentJson(dynamic json)
        {
            List<ProposalOverviewDto> proposalsResult = new List<ProposalOverviewDto>();
            foreach (dynamic proposal in json.proposals)
            {
                try
                {
                    var proposalResult = new ProposalOverviewDto();

                    var term = (JObject)proposal.terms;
                    dynamic priceDescription = term.Properties().FirstOrDefault() as JProperty;
                    
                    if (priceDescription == null || priceDescription.Value == null) continue;

                    if (priceDescription.Value.price == null ||
                        priceDescription.Value.currency == null ||
                        proposal.sign == null ||
                        proposal.validating_carrier == null)
                        continue;
                    
                    if (proposal.segment == null) 
                        continue;
                    
                    proposalResult.Price = priceDescription.Value.price;
                    proposalResult.Currency = priceDescription.Value.currency;
                    proposalResult.TicketSignId = proposal.sign;
                    proposalResult.ValidatingCarrierIconUrl = "http://pics.avs.io/300/150/" + proposal.validating_carrier + ".png";

                    var segments = new List<SegmentOverviewDto>();

                    foreach (dynamic segment in proposal.segment)
                    {
                        var flightInfo = segment.flight[0];

                        var segmentDto = MapSegmentFromJson(flightInfo);

                        if (segmentDto == null)
                            continue;

                        segments.Add(segmentDto);
                    }

                    var orderedSegments = segments.OrderBy(s => s.LocalDepatureTime);
                    proposalResult.ForwardTicketSegment = orderedSegments.ElementAtOrDefault(0);
                    proposalResult.ReturnTicketSegment = orderedSegments.ElementAtOrDefault(1);

                    proposalsResult.Add(proposalResult);
                }
                catch (Exception e)
                {
                    //TODO log
                }
            }

            return proposalsResult;
        }
    }
}

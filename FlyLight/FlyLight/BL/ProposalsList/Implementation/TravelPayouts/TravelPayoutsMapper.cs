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
        public static SegmentOverviewDto MapSegmentFromJson(JObject flightInfo)
        {
            try
            {
                long depatureTimestamp = flightInfo["local_departure_timestamp"].Value<long>();
                long arrivalTimestamp = flightInfo["local_arrival_timestamp"].Value<long>();
                var segmentDto = new SegmentOverviewDto
                {
                    DepartureAirport = flightInfo["departure"].Value<string>(),
                    LocalDepatureTime = UnixTimeToDateTime.ConvertUnixToDateTime(depatureTimestamp),
                    ArrivalAirport = flightInfo["arrival"].Value<string>(),
                    LocalArrivalTime = UnixTimeToDateTime.ConvertUnixToDateTime(arrivalTimestamp)
                };

                return segmentDto;
            }
            catch (Exception e)
            {
                //TODO log
                return null;
            }
        }

        public static ProposalOverviewDto MapFromProposalJson(JObject proposalJson)
        {
            try
            {
                var proposalResult = new ProposalOverviewDto();

                var term = (JObject)proposalJson["terms"];
                var priceDescriptionWrapper = term.Properties().FirstOrDefault();
                var priceDescription = (JObject)priceDescriptionWrapper.Value;

                proposalResult.Price = priceDescription["price"].Value<decimal>();
                proposalResult.Currency = priceDescription["currency"].Value<string>();
                proposalResult.TicketSignId = proposalJson["sign"].Value<string>();
                proposalResult.ValidatingCarrierIconUrl = "http://pics.avs.io/300/150/" + proposalJson["validating_carrier"].Value<string>() + ".png";

                var segments = new List<SegmentOverviewDto>();

                foreach (var segment in ((JArray)proposalJson["segment"]).OfType<JObject>())
                {
                    var flightInfo = (JObject)segment["flight"][0];

                    var segmentDto = MapSegmentFromJson(flightInfo);

                    if (segmentDto == null)
                        continue;

                    segments.Add(segmentDto);
                }

                var orderedSegments = segments.OrderBy(s => s.LocalDepatureTime);
                proposalResult.ForwardTicketSegment = orderedSegments.ElementAtOrDefault(0);
                proposalResult.ReturnTicketSegment = orderedSegments.ElementAtOrDefault(1);

                return proposalResult;
            }
            catch (Exception e)
            {
                //TODO log
                return null;
            }
        }

        public static IList<ProposalOverviewDto> MapFromAgentJson(JObject json)
        {
            try
            {
                List<ProposalOverviewDto> proposalsResult = new List<ProposalOverviewDto>();

                var proposalsJsonArray = json["proposals"] as JArray;
                if (proposalsJsonArray == null)
                    return new List<ProposalOverviewDto>();
                foreach (var proposal in proposalsJsonArray.OfType<JObject>())
                {
                    var proposalDto = MapFromProposalJson(proposal);
                    if (proposalDto == null)
                        continue;

                    proposalsResult.Add(proposalDto);
                }

                return proposalsResult;
            }
            catch (Exception e)
            {
                //TODO log
                return new List<ProposalOverviewDto>();
            }
        }
    }
}

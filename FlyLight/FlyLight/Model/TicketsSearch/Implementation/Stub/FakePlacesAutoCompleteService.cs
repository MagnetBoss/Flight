using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FlyLight.Model.TicketsSearch.Interfaces;

namespace FlyLight.Model.TicketsSearch.Implementation.Stub
{
    public class FakePlacesAutoCompleteService : IPlacesAutoCompleteService
    {
        public async Task<List<string>> GetPlaces(string term, string locale)
        {
            await Task.Delay(TimeSpan.FromSeconds(2)).ConfigureAwait(false);
            return GetAllCities().Where(x => x.StartsWith(term, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private IEnumerable<string> GetAllCities()
        {
            try
            {
                var assembly = typeof(FakePlacesAutoCompleteService).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream("FlyLight.Model.TicketsSearch.Implementation.Stub.airportcodes.xml");
                var xmlDeserializer = new XmlSerializer(typeof (iata));
                var converted = (iata)xmlDeserializer.Deserialize(stream);
                return converted.iata_airport_codes.Select(x => x.airport + " " + x.code).ToList();
            }
            catch (Exception e)
            {
            }
            return null;
        } 
    }


    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class iata
    {

        private iataIata_airport_codes[] iata_airport_codesField;

        /// <remarks/>
        [XmlElement("iata_airport_codes")]
        public iataIata_airport_codes[] iata_airport_codes
        {
            get
            {
                return this.iata_airport_codesField;
            }
            set
            {
                this.iata_airport_codesField = value;
            }
        }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public class iataIata_airport_codes
    {

        private string airportField;

        private string codeField;

        /// <remarks/>
        public string airport
        {
            get
            {
                return airportField;
            }
            set
            {
                airportField = value;
            }
        }

        /// <remarks/>
        public string code
        {
            get
            {
                return codeField;
            }
            set
            {
                codeField = value;
            }
        }
    }

}

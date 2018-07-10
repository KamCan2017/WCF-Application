using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class WorldWeatherService : IWeatherService
    {
        private readonly Dictionary<string, double> _citiesTemperature = new Dictionary<string, double>();

        public WorldWeatherService()
        {
            _citiesTemperature.Add("Yaounde", 35);
            _citiesTemperature.Add("Douala", 38);
            _citiesTemperature.Add("Berlin", 19.5);
        }

        public string GetTemperature(string city)
        {
            if(_citiesTemperature.TryGetValue(city, out double tempearture))
              return string.Format("Temperature in {0}: {1}°C", city, tempearture);

            return string.Format("No Temperature available for {0}.", city);
        }
    }
}

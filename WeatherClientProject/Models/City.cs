using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherClientProject.Models
{
    public class City
    {
        public int CityID { get; set; }
        public string Forecast { get; set; }
        public string Pincode { get; set; }
    }
}

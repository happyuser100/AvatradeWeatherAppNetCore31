using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class UserDetailsResponse
    {
        public string Email { get; set; }
        public string WeatherStateName { get; set; }
        public string UpdateDate { get; set; }
    }
}

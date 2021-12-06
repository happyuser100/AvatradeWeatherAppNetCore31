using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class Users
    {
        [JsonProperty("users")]
        public MyUser[] MyUsers { get; set; }
    }
}

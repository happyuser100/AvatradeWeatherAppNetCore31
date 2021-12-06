using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Models;
using System.IO;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherUserController : ControllerBase
    {

        [HttpGet]
        [Route("Test")]
        public async Task<ActionResult> Test()
        {
            await Task.Delay(1000);
            return Ok(JsonConvert.SerializeObject("Hello"));
        }

        [HttpGet]
        [Route("GetUserWeather")]
        //[HttpGet("GetUserWeather/{userName}")]
        public async Task<ActionResult> GetUserWeather(string userName)
        {
            string BaseUrl = @"https://www.metaweather.com/api/location/";
            UserDetailsResponse user = new UserDetailsResponse();
            //await Task.Delay(1000);

            var myJsonString = await System.IO.File.ReadAllTextAsync("users.json");
            var myJObject = JObject.Parse(myJsonString);

            //List<MyUser> myUsers = myJObject["users"] as List<MyUser>;

            var myJsonObject = JsonConvert.DeserializeObject<Users>(myJsonString);

            var myUser = myJsonObject.MyUsers.Where(x => x.Name.ToLower() == userName.ToLower()).FirstOrDefault();
            if (myUser == null)
                return BadRequest("User not exists");

            string woeid = myUser.Woeid.ToString();

            HttpClient client = new HttpClient();
            //string woeid = "2442047";
            client.BaseAddress = new Uri(BaseUrl + $"{woeid}");
            var data = await client.GetAsync("").Result.Content.ReadAsStringAsync();

            var myRootJsonObject =  JsonConvert.DeserializeObject<RootWeather>(data);

            DateTime now = DateTime.Now;
            user.Email = myUser.Email;
            user.WeatherStateName = myRootJsonObject.consolidated_weather[0].weather_state_name;
            user.UpdateDate = now.ToString();

            return Ok(JsonConvert.SerializeObject(user));
        }

    }
}

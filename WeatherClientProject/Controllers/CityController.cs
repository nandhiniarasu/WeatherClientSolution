using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WeatherClientProject.Models;

namespace WeatherClientProject.Controllers
{
    public class CityController : Controller
    {
        

        public async Task<ActionResult> Index()
        {
            string Baseurl = "http://localhost:44356/"; 
            var ProdInfo = new List<City>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Cities");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ProdResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    ProdInfo = JsonConvert.DeserializeObject<List<City>>(ProdResponse);
                }
                //returning the employee list to view  
                return View(ProdInfo);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(City sbt)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(sbt), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:44356/api/Cities", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<City>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Edit(int id)
        {
            TempData["City_ID"] = id;
            City b = new City();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:44356/api/Cities/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    b = JsonConvert.DeserializeObject<City>(apiResponse);
                }
            }
            return View(b);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(City b)
        {
            int vid = Convert.ToInt32(TempData["City_ID"]);
            b.CityID = vid;
            City receivedemp = new City();
            using (var httpClient = new HttpClient())
            {
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://localhost:44356/api/Cities/" + vid, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedemp = JsonConvert.DeserializeObject<City>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Weather")]
        public async Task<List<WeatherFC>> GetWeather()
        {
            string Baseurl = "http://localhost:44356/";
            var ProdInfo = new List<WeatherFC>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Cities/WeatherFC");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ProdResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    ProdInfo = JsonConvert.DeserializeObject<List<WeatherFC>>(ProdResponse);
                }
                //returning the employee list to view  
                return ProdInfo;
            }
        }

        [HttpGet]
        [Route("WeatherforCity")]
        public async Task<WeatherFC> GetWeatherForCity()
        {
            WeatherFC sba = new WeatherFC();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:44356/api/Cities/WeatherforCity?id=1"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    sba = JsonConvert.DeserializeObject<WeatherFC>(apiResponse);
                }
            }
            return sba;
        }
    }
}

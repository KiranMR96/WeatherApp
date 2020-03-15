using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers
{
   public class AccuWeatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweather.com/";
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
        public const string ONEDAYFORECAST_ENDPOINT = "forecasts/v1/daily/1day/{0}?apikey={1}";

        public const string API_KEY = "TpAF6pEhqPYLGeSgZQ4g1OAe8sdHfAuW";
        
        public static async Task<List<City>> GetCities(string query) 
        {
            List<City> cities = new List<City>();

            string url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT,API_KEY,query);

            using(HttpClient client=new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            CurrentConditions currentConditions = new CurrentConditions();
            string url = BASE_URL + string.Format(CURRENT_CONDITIONS_ENDPOINT, cityKey, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                currentConditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(json)).FirstOrDefault();
            }
            return currentConditions;
        }

        public static async Task<OnedayForeCast> GetOnedayForeCast( string cityKey)
        {
            OnedayForeCast onedayForeCast = new OnedayForeCast();
            string url = BASE_URL + string.Format(ONEDAYFORECAST_ENDPOINT, cityKey, API_KEY);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                onedayForeCast = (JsonConvert.DeserializeObject<List<OnedayForeCast>>(json)).FirstOrDefault();
            }
            return onedayForeCast;
        }
    }
}

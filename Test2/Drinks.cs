using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore;
using Newtonsoft.Json.Linq;

namespace Test2
{
    public class Drinks
    {
        private const string drinksUrl = "https://api.punkapi.com/v2/beers";
        private string urlParameters = "?page=";
        public List<string> GetDrinks(int page)
        {
            List<string> Drinks = new List<string>(0);
            var json = DrinksFromPage(urlParameters+page.ToString()).GetAwaiter().GetResult();
            foreach (var res in json)
            {
                Drinks.Add(GetNameOfDrink(res));
            }
            return Drinks;
        }

        public static async Task<dynamic> DrinksFromPage(string parameters)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(drinksUrl);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;

            response = client.GetAsync(parameters).Result;
            dynamic json = new JObject();
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                json = JsonConvert.DeserializeObject(result);
                return json;
            }
            return json;
        }

        public string GetNameOfDrink(dynamic drink)
        {
            foreach(var element in drink)
            {
                if (element.Name == "name")
                    return element.Value.ToString();
            }
            return "";
        }
    }
}

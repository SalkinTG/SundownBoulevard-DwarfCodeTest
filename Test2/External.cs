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
    public class External
    {
        public string Dish;
        public List<Tuple<int, string>> Drinks;

        private string callFailed = "Unable to get dish";

        private const string DishUrl = "https://www.themealdb.com/api/json/v1/1/random.php";
        private string urlParameters = "?api_key=123";

        public void GetDish()
        {
            var task = GetInfo().GetAwaiter().GetResult();
            Dish = GetDishNameFromJson(task);
        }


        public static async Task<JObject> GetInfo()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(DishUrl);
            DataTable responseObj = new DataTable();

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;

            response = client.GetAsync("").Result;
            dynamic json = new JObject();
            if (response.IsSuccessStatusCode)
            {
                string DishName;
                string result = response.Content.ReadAsStringAsync().Result;
                json = JsonConvert.DeserializeObject(result);
                return json;
            }
            return json;
        }

        public string GetDishNameFromJson(dynamic json)
        {
            string dishName = "";
            foreach (var res in json.meals[0])
            {
                if(res.Name == "strMeal")
                    {
                        return res.Value.ToString();
                    }
            }
            return dishName;
        }
    }
}

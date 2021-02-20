using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test2
{
    public class RandomDish
    {
        public string dish;

        private const string dishUrl = "https://www.themealdb.com/api/json/v1/1/random.php";

        public void GetDish()
        {
            var task = GetInfo().GetAwaiter().GetResult();
            dish = GetDishNameFromJson(task);
        }

        public static async Task<JObject> GetInfo()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(dishUrl);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;

            response = client.GetAsync("").Result;
            dynamic json = new JObject();
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                json = JsonConvert.DeserializeObject(result);
                return json;
            }
            return json;
        }

        public string GetDishNameFromJson(dynamic json)
        {
            string DishName = "";
            foreach (var res in json.meals[0])
            {
                if(res.Name == "strMeal")
                    {
                        return res.Value.ToString();
                    }
            }
            return DishName;
        }
    }
}

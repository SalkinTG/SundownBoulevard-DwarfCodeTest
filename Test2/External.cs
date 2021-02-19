using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Data;
using Newtonsoft.Json;

namespace Test2
{
    public class External
    {
        public string Dish;
        public List<Tuple<int, string>> Drinks;

        private string callFailed = "Unable to get dish";

        private const string DishUrl = "https://www.themealdb.com/api/json/v1/1/random.php";
        private string urlParameters = "?api_key=123";

        public string GetDish()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(DishUrl);
            DataTable responseObj = new DataTable();

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = new HttpResponseMessage();

            response = await client.GetAsync("api/WebApi").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                responseObj = JsonConvert.DeserializeObject<DataTable>(result);
                var value = response.Content;
                var dataObjects = response.Content.ReadAsAsync<JsonContractResolver>().Result;

                return "success";
            }
            else
            {
                return callFailed;
            }

        }
    }
}

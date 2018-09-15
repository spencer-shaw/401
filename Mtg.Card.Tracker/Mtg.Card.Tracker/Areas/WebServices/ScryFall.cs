using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mtg.Card.Tracker.Areas.WebServices
{
    public class ScryFall
    {
        private static readonly HttpClient client = new HttpClient();
        public RootObject GetCards(string cardName)
        {
            var repositories = ProcessRepositories(cardName);
            var data = JsonConvert.DeserializeObject<RootObject>(repositories);


            Console.WriteLine(data.colors);
            Console.WriteLine(data.name);
            Console.WriteLine(data.colors);
            Console.WriteLine(data.set);
            Console.WriteLine(data.id);
            Console.WriteLine(data.prints_search_uri);
            Console.WriteLine();

            var temp = 1;
            return data;
        }

        private static string ProcessRepositories(string cardName)
        {
            var url = "https://api.scryfall.com/cards/named?fuzzy=";
            url = url + cardName + "+com";

            HttpResponseMessage response;
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            response = client.SendAsync(request).Result;

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}

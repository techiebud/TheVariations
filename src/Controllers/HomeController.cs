using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace TheVariations.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> GetForumAPIKey(string apiKey, string user, string email, string pw, string callBack)
        {
            string result = "n/a";

            using (var client = new HttpClient())
            {
                // var baseUri = $"https://forum.variationscondos.com/register/setauthtoken?type=json&callback={callBack}&apikey=vBry7QpM9DM9kQliepqhS7reIuqBDGTF28OXlLvQ&user=Janie Sims&email=jwsims00@comcast.net&pwd=janie01";

                var baseUri = $"https://forum.variationscondos.com/register/setauthtoken?type=json&apikey={apiKey}&user={user}&email={email}&pw={pw}";
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.GetAsync(baseUri);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();

                    result = JsonConvert.ToString(responseJson);

                    //do something with the response here. Typically use JSON.net to deserialise it and work with it
                }
            }

            return Content(result);

        }
    }
}

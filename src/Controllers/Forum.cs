using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheVariations.Controllers
{
    [Route("api/[controller]")]
    public class Forum : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet("key")]
        public async Task<IActionResult> Get(string apiKey, string user, string email, string pw)
        {
            string result = "n/a";

            pw = AESEncrytDecry.DecryptStringAES(pw);


            using (var client = new HttpClient())
            {
                //var baseUri = "https://forum.variationscondos.com/register/setauthtoken?type=json&apikey=vBry7QpM9DM9kQliepqhS7reIuqBDGTF28OXlLvQ&user=Janie Sims&email=jwsims00@comcast.net&pw=janie01";

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

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value testing";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }


}



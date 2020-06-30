using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CauriPayment.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace CauriPayment.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _Config;

        public HomeController(IHttpClientFactory clientFactory, IConfiguration Config)
        {
            _clientFactory = clientFactory;
            _Config = Config;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromBody]PaymentTransaction transAction)
        {
            var url = "https://api.cauri.com/rest-v1/card/process";

            var client = _clientFactory.CreateClient();

            string[] transActionModel = new string[] { _Config["CauriKeys:project"], transAction.order_id, transAction.description, transAction.user, transAction.card_token, transAction.price, transAction.currency };
            Array.Sort(transActionModel, StringComparer.InvariantCulture);
            string createPipValue = "";
            foreach (var item in transActionModel)
            {
                if (string.IsNullOrEmpty(createPipValue))
                    createPipValue = item;
                else
                    createPipValue += "|" + item;
            }


            string key = _Config["CauriKeys:privateKey"];

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);

            byte[] messageBytes = encoding.GetBytes(createPipValue);
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            var signature = ByteToString(hashmessage);

            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("project", _Config["CauriKeys:project"]));
            nvc.Add(new KeyValuePair<string, string>("order_id", transAction.order_id));
            nvc.Add(new KeyValuePair<string, string>("description", transAction.description));
            nvc.Add(new KeyValuePair<string, string>("user", transAction.user));
            nvc.Add(new KeyValuePair<string, string>("card_token", transAction.card_token));
            nvc.Add(new KeyValuePair<string, string>("price", transAction.price));
            nvc.Add(new KeyValuePair<string, string>("currency", transAction.currency));
            nvc.Add(new KeyValuePair<string, string>("acs_return_url", transAction.acs_return_url));
            nvc.Add(new KeyValuePair<string, string>("recurring", transAction.recurring));
            nvc.Add(new KeyValuePair<string, string>("attr_server", transAction.attr_server));
            nvc.Add(new KeyValuePair<string, string>("attr_landing", transAction.attr_landing));
            //nvc.Add(new KeyValuePair<string, string>("recurring_interval", transAction.recurring_interval));
            nvc.Add(new KeyValuePair<string, string>("signature", signature));



            var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(nvc) };

            HttpResponseMessage messages = await client.SendAsync(req);

            var content = JsonConvert.SerializeObject(await messages.Content.ReadAsStringAsync());
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2").ToLower(); // hex format
            }
            return (sbinary);
        }

    }

}

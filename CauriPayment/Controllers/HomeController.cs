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
using System.Reflection;

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

        [HttpGet]
        public IActionResult Payment()
        {
            var model = new SCIModel
            {
                ik_co_id = _Config["SCI:co_id"],
                ik_suc_u = _Config["SCI:suc_u"],
                ik_fal_u = _Config["SCI:fal_u"],
                ik_pnd_u = _Config["SCI:pnd_u"],
                ik_pm_no = "ID_4233",
                ik_am = "1.44",
                ik_cur = "uah",
                ik_desc = "Payment Description"
            };
            return View(model);
        }

        public IActionResult Success([FromHeader] SCIModel model)
        {

            return View(model);
        }

        public IActionResult maxesh([FromHeader] SCIModel model)
        {

            return View(model);
        }

        public IActionResult Fail([FromForm] SCIModel model)
        {
            return View(model);
        }

        public IActionResult Pending([FromForm] SCIModel model)
        {
            return View(model);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> SCIPaymentPost([FromForm] SCIModel sCIModel)
        {
            var url = "https://sci.interkassa.com";

            var client = _clientFactory.CreateClient();

            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("ik_co_id", _Config["SCI:co_id"]));
            nvc.Add(new KeyValuePair<string, string>("ik_pm_no", sCIModel.ik_pm_no));
            nvc.Add(new KeyValuePair<string, string>("ik_am", sCIModel.ik_am));
            nvc.Add(new KeyValuePair<string, string>("ik_desc", sCIModel.ik_desc));
            nvc.Add(new KeyValuePair<string, string>("ik_act", sCIModel.ik_act));
            nvc.Add(new KeyValuePair<string, string>("ik_int", sCIModel.ik_int));
            nvc.Add(new KeyValuePair<string, string>("ik_cur", sCIModel.ik_cur));



            var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(nvc) };

            HttpResponseMessage messages = await client.SendAsync(req);

            var content = await messages.Content.ReadAsAsync<SCIPaymentResult>();

            return Ok(content);
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult Index([FromForm] SCIModel transAction)
        {
            string key = "itKqCZXDKSsQq7IE";

            IEnumerable<PropertyInfo> properties = transAction.GetType().GetProperties().OrderBy(d => d.Name);

            string createPipValue = "";
            foreach (var item in properties)
            {
                var name = item.Name;
                var value = item.GetValue(transAction);
                if (string.IsNullOrEmpty(createPipValue))
                {
                    if (value != null)
                        createPipValue = value.ToString();
                }
                else
                {
                    if (value != null)
                        createPipValue += ":" + value.ToString();
                }
            }

            createPipValue += ":" + key;
            var resultBytes = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(createPipValue));
            var base64Str = Convert.ToBase64String(resultBytes);
            return Ok(base64Str);
        }
        //[HttpPost]
        //public async Task<IActionResult> Index([FromBody]PaymentTransaction transAction)
        //{
        //    var url = "https://api.cauri.com/rest-v1/card/process";

        //    var client = _clientFactory.CreateClient();

        //    string[] transActionModel = new string[] { _Config["CauriKeys:project"], transAction.order_id, transAction.description, transAction.user, transAction.card_token, transAction.price, transAction.currency };
        //    Array.Sort(transActionModel, StringComparer.InvariantCulture);
        //    string createPipValue = "";
        //    foreach (var item in transActionModel)
        //    {
        //        if (string.IsNullOrEmpty(createPipValue))
        //            createPipValue = item;
        //        else
        //            createPipValue += "|" + item;
        //    }


        //    string key = _Config["CauriKeys:privateKey"];

        //    ASCIIEncoding encoding = new ASCIIEncoding();
        //    byte[] keyByte = encoding.GetBytes(key);

        //    HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);

        //    byte[] messageBytes = encoding.GetBytes(createPipValue);
        //    byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
        //    var signature = ByteToString(hashmessage);

        //    var nvc = new List<KeyValuePair<string, string>>();
        //    nvc.Add(new KeyValuePair<string, string>("project", _Config["CauriKeys:project"]));
        //    nvc.Add(new KeyValuePair<string, string>("order_id", transAction.order_id));
        //    nvc.Add(new KeyValuePair<string, string>("description", transAction.description));
        //    nvc.Add(new KeyValuePair<string, string>("user", transAction.user));
        //    nvc.Add(new KeyValuePair<string, string>("card_token", transAction.card_token));
        //    nvc.Add(new KeyValuePair<string, string>("price", transAction.price));
        //    nvc.Add(new KeyValuePair<string, string>("currency", transAction.currency));
        //    nvc.Add(new KeyValuePair<string, string>("acs_return_url", transAction.acs_return_url));
        //    nvc.Add(new KeyValuePair<string, string>("recurring", transAction.recurring));
        //    nvc.Add(new KeyValuePair<string, string>("attr_server", transAction.attr_server));
        //    nvc.Add(new KeyValuePair<string, string>("attr_landing", transAction.attr_landing));
        //    //nvc.Add(new KeyValuePair<string, string>("recurring_interval", transAction.recurring_interval));
        //    nvc.Add(new KeyValuePair<string, string>("signature", signature));

        //    var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(nvc) };

        //    HttpResponseMessage messages = await client.SendAsync(req);

        //    var content = JsonConvert.SerializeObject(await messages.Content.ReadAsStringAsync());
        //    return View();
        //}

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

        public IActionResult PaymentResult()
        {
            ViewBag.Desc = "1115070";
            ViewBag.CoId = "5ecbb2091ae1bd29008b4567";
            ViewBag.Am = "0.1";
            return View();
        }
        public JsonResult GetPaymentResult(SCIModel sCIModel)
        {
            string key = "itKqCZXDKSsQq7IE";

            IEnumerable<PropertyInfo> properties = sCIModel.GetType().GetProperties().OrderBy(d => d.Name);

            string createPipValue = "";
            foreach (var item in properties)
            {
                var name = item.Name;
                var value = item.GetValue(sCIModel);
                if (string.IsNullOrEmpty(createPipValue))
                {
                    if (value != null)
                        createPipValue = value.ToString();
                }
                else
                {
                    if (value != null)
                        createPipValue += ":" + value.ToString();
                }
            }

            createPipValue += ":" + key;
            var resultBytes = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(createPipValue));
            var base64Str = Convert.ToBase64String(resultBytes);
            return Json(new { sign = base64Str });
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CauriPayment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CauriPayment.Controllers
{
    public class CauriPaymentController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration Configuration;

        public CauriPaymentController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            Configuration = configuration;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DetermineUserId([FromForm]CreateUser model)
        {
            if (ModelState.IsValid)
            {
                var url = Configuration["Cauri:determineuserIdURL"];

                var client = _clientFactory.CreateClient();

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("project", Configuration["Cauri:project"]));
                nvc.Add(new KeyValuePair<string, string>("identifier", model.identifier));
                nvc.Add(new KeyValuePair<string, string>("display_name", model.display_name));
                nvc.Add(new KeyValuePair<string, string>("email", model.email));
                nvc.Add(new KeyValuePair<string, string>("phone", model.phone));
                nvc.Add(new KeyValuePair<string, string>("locale", model.locale));
                nvc.Add(new KeyValuePair<string, string>("ip", model.ip));
                nvc.Add(new KeyValuePair<string, string>("signature", model.signature));
                var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(nvc) };
                HttpResponseMessage messages = await client.SendAsync(req);
                HttpContext.Response.ContentType = "application/json";

                if (messages.IsSuccessStatusCode)
                {
                    var result = await messages.Content.ReadAsAsync<UserResponse>();
                    return Ok(result);
                }
                var content = await messages.Content.ReadAsAsync<ErrorResult>();
                return BadRequest(content);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> GetToken([FromForm]CreateToken model)
        {
            if (ModelState.IsValid)
            {
                var url = Configuration["Cauri:createTokenURL"];

                var client = _clientFactory.CreateClient();

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("project", Configuration["Cauri:project"]));
                nvc.Add(new KeyValuePair<string, string>("number", model.number));
                nvc.Add(new KeyValuePair<string, string>("expiration_month", model.expiration_month));
                nvc.Add(new KeyValuePair<string, string>("expiration_year", model.expiration_year));
                nvc.Add(new KeyValuePair<string, string>("security_code", model.security_code));

                var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(nvc) };
                HttpResponseMessage messages = await client.SendAsync(req);
                HttpContext.Response.ContentType = "application/json";

                if (messages.IsSuccessStatusCode)
                {
                    var result = await messages.Content.ReadAsAsync<TokenResult>();
                    return Ok(result);
                }
                var content = await messages.Content.ReadAsAsync<ErrorResult>();
                return BadRequest(content);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> PaymentTransAction([FromForm]PaymentTransaction model)
        {
            if (ModelState.IsValid)
            {
                var url = Configuration["Cauri:processCardURL"];

                var client = _clientFactory.CreateClient();

                string[] modelModel = new string[] { Configuration["Cauri:project"],
                model.order_id,
                model.description,
                model.user,
                model.card_token,
                model.price,
                model.currency,
                model.acs_return_url,
                model.recurring,
                model.attr_server,
                model.attr_landing    };

                Array.Sort(modelModel, StringComparer.InvariantCulture);
                string createPipValue = "";
                foreach (var item in modelModel)
                {
                    if (string.IsNullOrEmpty(createPipValue))
                    {
                        if (item != "")
                            createPipValue = item;
                    }
                    else
                    {
                        if (item != "")
                            createPipValue += "|" + item;
                    }
                }

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("project", Configuration["Cauri:project"]));
                nvc.Add(new KeyValuePair<string, string>("order_id", model.order_id));
                nvc.Add(new KeyValuePair<string, string>("description", model.description));
                nvc.Add(new KeyValuePair<string, string>("user", model.user));
                nvc.Add(new KeyValuePair<string, string>("card_token", model.card_token));
                nvc.Add(new KeyValuePair<string, string>("price", model.price));
                nvc.Add(new KeyValuePair<string, string>("currency", model.currency));
                nvc.Add(new KeyValuePair<string, string>("signature", GenerateSignature(createPipValue)));

                if (!string.IsNullOrEmpty(model.acs_return_url))
                    nvc.Add(new KeyValuePair<string, string>("acs_return_url", model.acs_return_url));

                if (!string.IsNullOrEmpty(model.recurring))
                    nvc.Add(new KeyValuePair<string, string>("recurring", model.recurring));

                if (!string.IsNullOrEmpty(model.attr_server))
                    nvc.Add(new KeyValuePair<string, string>("attr_server", model.attr_server));

                if (!string.IsNullOrEmpty(model.attr_landing))
                    nvc.Add(new KeyValuePair<string, string>("attr_landing", model.attr_landing));

                //if (!string.IsNullOrEmpty(model.recurring_interval))
                //    nvc.Add(new KeyValuePair<string, string>("recurring_interval", model.recurring_interval));



                var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(nvc) };
                HttpResponseMessage messages = await client.SendAsync(req);
                HttpContext.Response.ContentType = "application/json";

                if (messages.IsSuccessStatusCode)
                {
                    var result = await messages.Content.ReadAsAsync<PaymentResult>();
                    return Ok(result);
                }
                var content = await messages.Content.ReadAsAsync<ErrorResult>();
                return BadRequest(content);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Reverse([FromForm]Reverse model)
        {
            if (ModelState.IsValid)
            {
                var url = Configuration["Cauri:reverseURL"];

                var client = _clientFactory.CreateClient();

                string[] modelModel = new string[] { Configuration["Cauri:project"],
                model.id,
                model.comment,
                model.order_id};

                Array.Sort(modelModel, StringComparer.InvariantCulture);
                string createPipValue = "";
                foreach (var item in modelModel)
                {
                    if (string.IsNullOrEmpty(createPipValue))
                    {
                        if (item != "")
                            createPipValue = item;
                    }
                    else
                    {
                        if (item != "")
                            createPipValue += "|" + item;
                    }
                }



                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("project", Configuration["Cauri:project"]));
                if (!string.IsNullOrEmpty(model.id))
                    nvc.Add(new KeyValuePair<string, string>("id", model.id));
                if (!string.IsNullOrEmpty(model.comment))
                    nvc.Add(new KeyValuePair<string, string>("comment", model.comment));
                if (!string.IsNullOrEmpty(model.order_id))
                    nvc.Add(new KeyValuePair<string, string>("comment", model.order_id));

                nvc.Add(new KeyValuePair<string, string>("signature", GenerateSignature(createPipValue)));


                var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(nvc) };
                HttpResponseMessage messages = await client.SendAsync(req);
                HttpContext.Response.ContentType = "application/json";

                if (messages.IsSuccessStatusCode)
                {
                    var result = await messages.Content.ReadAsAsync<ReverseResult>();
                    return Ok(result);
                }
                var content = await messages.Content.ReadAsAsync<ErrorResult>();
                return BadRequest(content);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Status([FromForm]Status model)
        {
            if (ModelState.IsValid)
            {
                var url = Configuration["Cauri:statusURL"];

                var client = _clientFactory.CreateClient();

                string[] modelModel = new string[] { Configuration["Cauri:project"],
                model.id,
                model.order_id};

                Array.Sort(modelModel, StringComparer.InvariantCulture);
                string createPipValue = "";
                foreach (var item in modelModel)
                {
                    if (string.IsNullOrEmpty(createPipValue))
                    {
                        if (item != "")
                            createPipValue = item;
                    }
                    else
                    {
                        if (item != "")
                            createPipValue += "|" + item;
                    }
                }

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("project", Configuration["Cauri:project"]));

                if (!string.IsNullOrEmpty(model.id))
                    nvc.Add(new KeyValuePair<string, string>("id", model.id));

                if (!string.IsNullOrEmpty(model.order_id))
                    nvc.Add(new KeyValuePair<string, string>("order_id", model.order_id));

                nvc.Add(new KeyValuePair<string, string>("signature", GenerateSignature(createPipValue)));


                var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(nvc) };
                HttpResponseMessage messages = await client.SendAsync(req);
                HttpContext.Response.ContentType = "application/json";

                if (messages.IsSuccessStatusCode)
                {
                    var result = await messages.Content.ReadAsAsync<StatusResult>();
                    return Ok(result);
                }
                var content = await messages.Content.ReadAsAsync<ErrorResult>();
                return BadRequest(content);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Refund([FromForm]Refund model)
        {
            if (ModelState.IsValid)
            {
                var url = Configuration["Cauri:refundURL"];

                var client = _clientFactory.CreateClient();

                string[] modelModel = new string[] { Configuration["Cauri:project"],
                model.id,
                model.amount,
                model.comment,
                model.order_id};

                Array.Sort(modelModel, StringComparer.InvariantCulture);
                string createPipValue = "";
                foreach (var item in modelModel)
                {
                    if (string.IsNullOrEmpty(createPipValue))
                    {
                        if (item != "")
                            createPipValue = item;
                    }
                    else
                    {
                        if (item != "")
                            createPipValue += "|" + item;
                    }
                }



                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("project", Configuration["Cauri:project"]));
                if (!string.IsNullOrEmpty(model.id))
                    nvc.Add(new KeyValuePair<string, string>("id", model.id));
                if (!string.IsNullOrEmpty(model.amount))
                    nvc.Add(new KeyValuePair<string, string>("amount", model.amount));
                if (!string.IsNullOrEmpty(model.comment))
                    nvc.Add(new KeyValuePair<string, string>("comment", model.comment));
                if (!string.IsNullOrEmpty(model.order_id))
                    nvc.Add(new KeyValuePair<string, string>("order_id", model.order_id));

                nvc.Add(new KeyValuePair<string, string>("signature", GenerateSignature(createPipValue)));


                var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(nvc) };
                HttpResponseMessage messages = await client.SendAsync(req);
                HttpContext.Response.ContentType = "application/json";

                if (messages.IsSuccessStatusCode)
                {
                    var result = await messages.Content.ReadAsAsync<RefundResult>();
                    return Ok(result);
                }
                var content = await messages.Content.ReadAsAsync<ErrorResult>();
                return BadRequest(content);
            }
            return BadRequest();
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

        /// <summary>
        /// This method generate the signature by string pipe values
        /// </summary>
        /// <param name="createPipValue"></param>
        /// <returns></returns>
        private string GenerateSignature(string createPipValue)
        {
            string key = Configuration["Cauri:privateKey"];

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);

            byte[] messageBytes = encoding.GetBytes(createPipValue);
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return ByteToString(hashmessage);

        }
    }
}
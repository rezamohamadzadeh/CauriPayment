using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CauriPayment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CauriPayment.Controllers
{
    public class SCIPaymentController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration Configuration;

        public SCIPaymentController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            Configuration = configuration;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet] 
        public async Task<IActionResult> GetCurrency()
        {
            var url = Configuration["SCI:GetCurrencies"];

            var client = _clientFactory.CreateClient();

            HttpResponseMessage messages = await client.GetAsync(url);
            HttpContext.Response.ContentType = "application/json";

            if (messages.IsSuccessStatusCode)
            {
                var result = await messages.Content.ReadAsAsync<SCIResult>();
                return Ok(result);
            }
            return BadRequest();
        }


    }
}
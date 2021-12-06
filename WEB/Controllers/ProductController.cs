using Newtonsoft.Json;
using RESPONSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WEB.Models;

namespace WEB.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public async Task<ActionResult> Index()
        {
            List<ProductModel> model = new List<ProductModel>();

            var client = new HttpClient();
            var urlBase = "https://localhost:44314";

            client.BaseAddress = new Uri(urlBase);
            var url = string.Concat(urlBase, "/api", "/Products");


            var response = client.GetAsync(url).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                //De JSON a Response
                var people = JsonConvert.DeserializeObject<List<ProductResponse>>(result);

                //De Response a Model
                model = (from c in people
                         select new ProductModel
                         {
                             Name = c.Name,
                             FinalPrice = c.Price - c.Price*c.Discount,
                             Expiry = string.Concat(
                                 "Expira en ",
                                 c.DateOfExpiry.Day-DateTime.Today.Day,
                                 " día/s.")
                         }).ToList();
            }

            return View(model);
        }


        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProductResponse model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var urlBase = "https://localhost:44314";
                client.BaseAddress = new Uri(urlBase);
                var url = string.Concat(urlBase, "/api", "/Products");

                var response = client.PostAsync(url, content).Result;

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    var result = await response.Content.ReadAsStringAsync();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
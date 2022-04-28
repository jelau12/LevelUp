using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using LevelUp.Web.Models;
using System.Net.Http.Json;

namespace LevelUp.Web.Controllers
{
    public class ProductController : Controller
    {
        string Baseurl = "http://localhost:61723";

        // GET: Products
        #region Consume Get Method
        public async Task<IActionResult> Index()
        {
            List<Product> products = new List<Product>();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetProducts using HttpClient
                HttpResponseMessage responeMsg = await client.GetAsync("/api/Products/GetAllProducts");

                //Checking the response is successful or not which is sent using HttpClient
                if (responeMsg.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var productResponse = responeMsg.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    products = JsonConvert.DeserializeObject<List<Product>>(productResponse);
                }
            }

            return View(products);
        }
        #endregion

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        #region Consume Post Method
        public IActionResult Create(Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + "/api/Products/Create");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("Create", product);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error.");

            return View(product);
        } 
        #endregion
    }
}
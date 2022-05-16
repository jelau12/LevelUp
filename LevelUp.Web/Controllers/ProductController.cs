using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;
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

        #region Consume Delete method
        public IActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("/api/Products/Delete/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        #endregion

        public IActionResult Edit(int id)
        {
            Product product = null;

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                //HTTP GET
                var responseTask = client.GetAsync("/api/Products/GetProductById/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;

                //Checking the response is successful or not which is sent using HttpClient
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product>();
                    readTask.Wait();

                    product = readTask.Result;
                }
            }
            return View(product);
        }

        [HttpPost]
        #region Consume put method
        public IActionResult Edit(Product product)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl + "/api/Products/Edit");

                //Send a PUT request to the specified Uri
                var putTask = client.PutAsJsonAsync<Product>("Edit", product);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        } 
        #endregion

    }
}
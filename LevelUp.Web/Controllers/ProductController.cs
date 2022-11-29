using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using LevelUp.Web.Models;
using System.Net.Http.Json;
using LevelUp.Entities.Models;
using System.Collections;
using LevelUp.Services;

namespace LevelUp.Web.Controllers
{
    public class ProductController : Controller
    {
        string Baseurl = "http://localhost:61723";

        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Calls GetAllProducts endpoint and gets all products
        /// </summary>
        /// <returns>View with list of all products</returns>
        // GET: Products
        #region Consume Get Method
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllAsync();

            return View(result);
        }
        #endregion

        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Calls Create endpoint and creates a new product
        /// </summary>
        /// <param name="product"></param>
        [HttpPost]
        #region Consume Post Method
        public async Task<IActionResult> Create(Product product)
        {
            bool result = await _service.CreateProductAsync(product);

            if (result == true)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Server Error.");

            return View(product);
        }
        #endregion

        /// <summary>
        /// Calls Delete endpoint and deletes product with <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        #region Consume Delete method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _service.DeleteProductAsync(id);

            if (result == true)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        #endregion

        /// <summary>
        /// Calls GetProductById endpoint and gets product with <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        #region Consume Get method
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
        #endregion

        /// <summary>
        /// Calls Edit endpoint and updates the <paramref name="product"/>
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        #region Consume Edit method
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
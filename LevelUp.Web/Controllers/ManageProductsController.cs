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
    public class ManageProductsController : Controller
    {
        string Baseurl = "http://localhost:61723";

        private readonly ProductService _productService;

        public ManageProductsController(ProductService service)
        {
            _productService = service;
        }

        /// <summary>
        /// Calls GetAllProducts endpoint and gets all products
        /// </summary>
        /// <returns>View with list of all products</returns>
        // GET: Products
        #region Consume Get Method
        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAllAsync();

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
            bool result = await _productService.CreateProductAsync(product);

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

            var result = await _productService.DeleteProductAsync(id);

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
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _productService.GetByIdAsync(id);

            if(response == null)
            {
                return NotFound();
            }
            return View(response);
        }
        #endregion

        /// <summary>
        /// Calls Edit endpoint and updates the <paramref name="product"/>
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        #region Consume Edit method
        public async Task<IActionResult> Edit(Product product)
        {
            var result = await _productService.EditProductAsync(product);
            if (result == true)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }
        #endregion
    }
}
using LevelUp.Entities.Models;
using LevelUp.Services.Base;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace LevelUp.Services
{
    public class ProductService : WebServicesBase
    {
        protected const string baseUrl = "http://localhost:61723/api/Products/";

        public virtual async Task<List<Product>> GetAllAsync()
        {

            string json = await CallEndpointAsync(baseUrl);

            List<Product> products = JsonSerializer.Deserialize<List<Product>>(json);

            return products;
        }

        public virtual async Task<Product> GetByIdAsync(int id)
        {
            string json = await CallEndpointAsync(baseUrl);

            Product productById = JsonSerializer.Deserialize<Product>(json);

            return productById;
        }

        public virtual async Task<bool> CreateProductAsync(Product product)
        {
            var result = await CallCreateProductAsync(baseUrl, product);

            return result;
        }
    }
}
using LevelUp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LevelUp.Services.Base
{
    public class WebServicesBase
    {
        static readonly HttpClient httpClient = new();
        public virtual async Task<string> CallEndpointAsync(string url)
        {
            try
            {
                using HttpResponseMessage response = await httpClient.GetAsync(url + "GetAllProducts");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            catch(Exception ex)
            {
                throw new ArgumentException("Could not establish connection to endpoint", ex);
            }
        }
        public virtual async Task<bool> CallCreateProductAsync(string url, Product product)
        {
            try
            {
                var postTask = await httpClient.PostAsJsonAsync(url + "Create", product);

                if (postTask.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Could not establish connection to endpoint", ex);
            }
        }

        public virtual async Task<bool> CallDeleteProductAsync(string url, int? id)
        {
            try
            {
                var deleteTask = await httpClient.DeleteAsync(url + "Delete/" + id.ToString());

                if (deleteTask.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Could not establish connection to endpoint", ex);
            }
        }
    }
}
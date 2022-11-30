using LevelUp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LevelUp.Web.Controllers
{
    public class EcommerceController : Controller
    {
        private readonly ProductService _productService;

        public EcommerceController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //http://localhost:51219/Ecommerce/ProductDetails/2
        public async Task<IActionResult> ProductDetails(int id)
        {
            var response = await _productService.GetByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }
    }
}

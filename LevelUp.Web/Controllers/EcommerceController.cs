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

        public async Task<IActionResult> Index()
        {
            var response = await _productService.GetAllAsync();

            if(response == null)
            {
                return NotFound();
            }

            return View(response);
        }

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

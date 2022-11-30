using Microsoft.AspNetCore.Mvc;

namespace LevelUp.Web.Controllers
{
    public class EcommerceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

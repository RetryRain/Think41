using Microsoft.AspNetCore.Mvc;

namespace Think41.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Simple_Employee_Management.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

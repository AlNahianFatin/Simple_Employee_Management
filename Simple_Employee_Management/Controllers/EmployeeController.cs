using Microsoft.AspNetCore.Mvc;

namespace Simple_Employee_Management.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Simple_Employee_Management.Data;
using Simple_Employee_Management.DTO;

namespace Simple_Employee_Management.Controllers
{
    public class CredentialController : Controller
    {
        private SimpleEmployeeManagementDbContext _context;
        private IMapper _mapper;

        public CredentialController(SimpleEmployeeManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            var sessionEmail = HttpContext.Session.GetString("Email");
            var cookieEmail = Request.Cookies["Email"];

            if (cookieEmail != null && sessionEmail != null)
                return RedirectToAction("Index", "Home");

            var model = new LoginDTO();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var validUser = _context.Admins.FirstOrDefault(e => e.Email == dto.Email);

            if (validUser == null || validUser.Password != dto.Password.Trim())
            {
                ViewBag.Error = "Invalid Credentials!";
                return View(dto);
            }

            HttpContext.Session.SetString("Email", dto.Email);

            if (dto.remember)
            {
                var rememberOption = new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(5),
                    HttpOnly = true
                };
                Response.Cookies.Append("Email", dto.Email, rememberOption);
            }
            else
            {
                var option = new CookieOptions()
                {
                    HttpOnly = true
                };
                Response.Cookies.Append("Email", dto.Email, option);
            }

            TempData["Email"] = dto.Email;

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("Email");

            TempData["Email"] = null;

            return Redirect("Login");
        }

        public IActionResult Denied()
        {
            return View();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Simple_Employee_Management.Data;
using Simple_Employee_Management.Data.Entities;
using Simple_Employee_Management.DTO;

namespace Simple_Employee_Management.Controllers
{
    public class AdminController : Controller
    {
        private SimpleEmployeeManagementDbContext _context;
        private IMapper _mapper;

        public AdminController(SimpleEmployeeManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            if (Request.Cookies["Email"] != null)
                TempData["Email"] = Request.Cookies["Email"];

            var allAdmins = _context.Admins.ToList();
            var dto = _mapper.Map<List<AdminDTO>>(allAdmins);
            return View(dto);
        }

        public IActionResult Add(int AdminId)
        {
            if (Request.Cookies["Email"] != null)
                TempData["Email"] = Request.Cookies["Email"];

            return View(new AdminDTO());
        }

        [HttpPost]
        public IActionResult Add(AdminDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            try
            {
                var admin = _mapper.Map<Admin>(dto);

                _context.Admins.Add(admin);
                TempData["Success"] = "Admin added successfully.";

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error adding admin: " + (ex.Message);
                return View(dto);
            }
        }

        public IActionResult Update(int AdminId)
        {
            if (Request.Cookies["Email"] != null)
                TempData["Email"] = Request.Cookies["Email"];

            try
            {
                var admin = _context.Admins.Find(AdminId) ?? new Admin();
                var dto = _mapper.Map<AdminDTO>(admin);
                return View(dto);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Something went wrong while fetching the admin: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Update(AdminDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            try
            {
                var admin = _mapper.Map<Admin>(dto);

                var existingAdmin = _context.Admins.Find(admin.AdminId);
                if (existingAdmin != null)
                {
                    existingAdmin.Username = admin.Username;
                    existingAdmin.Password = admin.Password;
                    existingAdmin.Email = admin.Email;

                    _context.Admins.Update(existingAdmin);
                    TempData["Success"] = "Admin updated successfully.";
                }
                else
                {
                    TempData["Error"] = "Admin not found for update.";
                    return View(dto);
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error saving admin: " + (ex.Message);
                return View(dto);
            }
        }

        //public IActionResult Delete(int AdminId)
        //{
        //    try
        //    {
        //        var admin = _context.Admins.Find(AdminId);
        //        if (admin != null)
        //        {
        //            _context.Admins.Remove(admin);
        //            _context.SaveChanges();
        //            TempData["Success"] = $"Admin deleted successfully.";
        //        }
        //        else
        //            TempData["Error"] = "Admin not found.";
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error"] = "Something went wrong while deleting the admin: " + ex.Message;
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}
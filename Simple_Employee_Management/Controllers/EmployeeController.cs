using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Simple_Employee_Management.Data;
using Simple_Employee_Management.Data.Entities;
using Simple_Employee_Management.DTO;

namespace Simple_Employee_Management.Controllers
{
    public class EmployeeController : Controller
    {
        private SimpleEmployeeManagementDbContext _context;
        private IMapper _mapper;

        public EmployeeController(SimpleEmployeeManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            if (Request.Cookies["Email"] == null && HttpContext.Session.GetString("Email") == null)
                return RedirectToAction("Denied", "Credential");

            if (Request.Cookies["Email"] != null)
                TempData["Email"] = Request.Cookies["Email"];

            var allEmployees = _context.Employees.ToList();
            var dto = _mapper.Map<List<EmployeeDTO>>(allEmployees);
            return View(dto);
        }

        public IActionResult Add(int Id)
        {
            if (Request.Cookies["Email"] == null && HttpContext.Session.GetString("Email") == null)
                return RedirectToAction("Denied", "Credential");

            if (Request.Cookies["Email"] != null)
                TempData["Email"] = Request.Cookies["Email"];

            return View(new EmployeeDTO());
        }

        [HttpPost]
        public IActionResult Add(EmployeeDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            try
            {
                var employee = _mapper.Map<Employee>(dto);

                _context.Employees.Add(employee);
                TempData["Success"] = "Employee added successfully.";

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error adding employee: " + (ex.Message);
                return View(dto);
            }
        }

        public IActionResult Update(int Id)
        {
            if (Request.Cookies["Email"] == null && HttpContext.Session.GetString("Email") == null)
                return RedirectToAction("Denied", "Credential");

            if (Request.Cookies["Email"] != null)
                TempData["Email"] = Request.Cookies["Email"];

            try
            {
                var employee = _context.Employees.Find(Id) ?? new Employee();
                var dto = _mapper.Map<EmployeeDTO>(employee);
                return View(dto);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Something went wrong while fetching the employee: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Update(EmployeeDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            try
            {
                var employee = _mapper.Map<Employee>(dto);

                var existingEmployee = _context.Employees.Find(employee.Id);
                if (existingEmployee != null)
                {
                    existingEmployee.FirstName = employee.FirstName;
                    existingEmployee.LastName = employee.LastName;
                    existingEmployee.Position = employee.Position;
                    existingEmployee.Salary = employee.Salary;

                    _context.Employees.Update(existingEmployee);
                    TempData["Success"] = "Employee updated successfully.";
                }
                else
                {
                    TempData["Error"] = "Employee not found for update.";
                    return View(dto);
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error saving employee: " + (ex.Message);
                return View(dto);
            }
        }

        public IActionResult Delete(int Id)
        {
            try
            {
                var employee = _context.Employees.Find(Id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                    TempData["Success"] = $"Employee {employee.FirstName} {employee.LastName} deleted successfully.";
                }
                else
                    TempData["Error"] = "Employee not found.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Something went wrong while deleting the employee: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
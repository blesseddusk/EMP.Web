
using EMP.Web.Models;
using EMP.Web.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace EMP.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmpContext _context;

        public HomeController(ILogger<HomeController> logger, EmpContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetEmployeeList()
        {
            try
            {
                IEnumerable<Employee> employeeList = from emp in _context.TblEmployees
                                                     join dept in _context.TblDepartments on emp.DeptId equals dept.Id
                                                     select new Employee
                                                     {
                                                         Id = emp.Id,
                                                         EmpName = emp.EmpName,
                                                         DeptId = emp.DeptId,
                                                         DeptName = dept.DeptName,
                                                         Address = emp.Address,
                                                         Email = emp.Email,
                                                         CreatedDate = emp.CreatedDate,
                                                         UpdatedDate = emp.UpdatedDate
                                                     };

                return Json(employeeList.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError("Home Controller - GetEmployeeList => ", ex);
                return Json(null);
            }
        }

        public JsonResult GetDepartmentList()
        {
            try
            {
                IEnumerable<Department> departmentList = from dept in _context.TblDepartments
                                                         select new Department
                                                         {
                                                             Id = dept.Id,
                                                             DeptName = dept.DeptName,
                                                         };

                return Json(departmentList.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError("Home Controller => GetDepartmentList => ", ex);
                return Json(null);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult UpdateEmployee(Employee employee)
        {
            try
            {
                TblEmployee? oldEmployee = _context.TblEmployees
                    .Where(W => W.Id == employee.Id)
                    .FirstOrDefault();

                if (oldEmployee == null)
                {
                    throw new Exception("Employess does not exist.");
                }

                oldEmployee.EmpName = employee.EmpName;
                oldEmployee.DeptId = employee.DeptId;
                oldEmployee.Address = employee.Address;
                oldEmployee.Email = employee.Email;
                oldEmployee.UpdatedDate = DateTime.Now;

                _context.TblEmployees.Update(oldEmployee);
                _context.SaveChanges();

                return Json(new ResultInfo() { Result = true });
            }
            catch (Exception ex)
            {
                _logger.LogError("Home Controller - UpdateEmployee => ", ex);
                return Json(new ResultInfo() { Result = false, ErrorInfo = new ErrorInfo() { ErrMessage = ex.Message } });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            try
            {
                TblEmployee newEmployee = new TblEmployee
                {
                    EmpName = employee.EmpName,
                    DeptId = employee.DeptId,
                    Address = employee.Address,
                    Email = employee.Email,
                    CreatedDate = DateTime.Now
                };

                await _context.TblEmployees.AddAsync(newEmployee);
                await _context.SaveChangesAsync();

                return Json(new ResultInfo() { Result = true });
            }
            catch (Exception ex)
            {
                _logger.LogError("Home Controller - AddEmployee => ", ex);
                return Json(new ResultInfo() { Result = false, ErrorInfo = new ErrorInfo() { ErrMessage = ex.Message } });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult DeleteEmployee(long empID)
        {
            try
            {
                TblEmployee? oldEmployee = _context.TblEmployees
                    .Where(W => W.Id == empID)
                    .FirstOrDefault();

                if (oldEmployee == null)
                {
                    throw new Exception("Employess does not exist.");
                }
                _context.TblEmployees.Remove(oldEmployee);
                _context.SaveChanges();

                return Json(new ResultInfo() { Result = true });
            }
            catch (Exception ex)
            {
                _logger.LogError("Home Controller - DeleteEmployee => ", ex);
                return Json(new ResultInfo() { Result = false, ErrorInfo = new ErrorInfo() { ErrMessage = ex.Message } });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
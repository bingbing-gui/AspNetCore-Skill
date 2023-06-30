using AspNetCore.Views.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Views.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(int id, string name)
        {
            string welcomeMessage = $"欢迎员工: {name} 编号: {id}";
            return View((object)welcomeMessage);
        }
        [HttpGet]
        public IActionResult Detail()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Detail(int id, string name)
        {
            
            var employee = new Employee();
            employee.Id = id;
            employee.Name = name;
            employee.Salary = 1000;
            employee.Designation = "Manager";
            employee.Address = "New York";
            return View(employee);
        }
    }
}

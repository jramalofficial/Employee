using Employee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            DatabaseConn emp = new DatabaseConn();
            List<Employee.Models.Entity.Employee> employee=emp.GetAllEmployee();
            return View(employee);
        }
    }
}
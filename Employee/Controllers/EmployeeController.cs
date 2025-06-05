using Employee.Models;
using Employee.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {
        
        DatabaseConn emp = new DatabaseConn();
        public ActionResult Index()
        {
            
            List<Employee.Models.Entity.Employee> employee=emp.GetAllEmployee();
            return View(employee);
        }
        public JsonResult GetAll()
        {
            var employees = emp.GetAllEmployee(); 
            return Json(new { data = employees }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetAllDepartment()
        {
            var department = emp.GetDepartments();
            return Json(new { data = department },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(Employee.Models.Entity.Employee employee)
        {
            var db = new DatabaseConn();
            db.SaveEmployee(employee);

            return RedirectToAction("Index");
        }
    }
}
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
            return View();
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

        public JsonResult EditEmployee(int id)
        {
            var db=new DatabaseConn();
            Employee.Models.Entity.Employee emp=db.Edit(id);

            return Json(new { data=emp},JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(Employee.Models.Entity.Employee employee)
        {
            var db = new DatabaseConn();
            db.UpdateEmployee(employee);
            return RedirectToAction("Index");
        }

        public JsonResult Delete(int id)
        {
            var db = new DatabaseConn();
            db.DeleteEmployee(id);
            return Json(new {success=true},JsonRequestBehavior.AllowGet);
        }
    }


}
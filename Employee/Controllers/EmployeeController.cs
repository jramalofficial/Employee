using Employee.Models;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
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
                return Json(new { data = department }, JsonRequestBehavior.AllowGet);
            
      
        }

        [HttpPost]
        public ActionResult Add(Employee.Models.Entity.Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    emp.SaveEmployee(employee);
                    return Json(new { success = true });

                }
                else
                {

                    var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .Select(x => new { Field = x.Key, Error = x.Value.Errors.First().ErrorMessage })
                        .ToList();

                    return Json(new { success = false, errors });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        
        public JsonResult EditEmployee(int id)
        {
            
                var db = new DatabaseConn();
                Employee.Models.Entity.Employee emp = db.Edit(id);


                string formattedDob = emp.Dob.ToString("yyyy-MM-dd");

                var result = new
                {
                    data = new
                    {
                        emp.Id,
                        emp.FirstName,
                        emp.MiddleName,
                        emp.LastName,
                        Dob = formattedDob,
                        emp.Email,
                        emp.Phone,
                        emp.StreetAddress,
                        emp.City,
                        emp.State,
                        emp.Country,
                        emp.ZipCode,
                        emp.DeptId
                    }
                };
                return Json(result, JsonRequestBehavior.AllowGet);

                //return Json(new { data=emp},JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        public ActionResult Update(Employee.Models.Entity.Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    emp.UpdateEmployee(employee);
                    return Json(new { success = true });

                }
                else
                {
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .Select(x => new { Field = x.Key, Error = x.Value.Errors.First().ErrorMessage })
                        .ToList();

                    return Json(new { success = false, errors });
                }
                
            }
            catch(SqlException ex)
            {
                return Json(new { success = false, message = ex.Message});
            }
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                emp.DeleteEmployee(id);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch(SqlException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            
            
        }
    }


}
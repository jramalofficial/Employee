using Employee.Models;
using Employee.Models.Entity;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {
        
        DatabaseConn db = new DatabaseConn();
        public ActionResult Index()
        {      
            return View();
        }


        [HttpGet]
        public JsonResult GetAll()
        {
            try
            {
                var employees = db.GetAllEmployee();
                return Json(new { success=true, data = employees }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) 
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult GetAllDepartment()
        {
            try
            {
                var department = db.GetDepartments();
                return Json(new { success=true,data = department }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message=ex.Message }, JsonRequestBehavior.AllowGet);
            }    
        }




        [HttpPost]
        public ActionResult Add(Employee.Models.Entity.Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.SaveEmployee(employee);
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



        [HttpGet]
        public JsonResult EditEmployee(int id)
        {
            try
            {
                Employee.Models.Entity.Employee emp = db.Edit(id);             
                var result = new
                {
                    data = new
                    {
                        emp.Id,
                        emp.FirstName,
                        emp.MiddleName,
                        emp.LastName,
                        Dob = emp.Dob.ToString("yyyy-MM-dd"),
                        emp.Email,
                        emp.Phone,
                        emp.StreetAddress,
                        emp.City,
                        emp.State,
                        emp.Country,
                        emp.ZipCode,
                        emp.DeptId
                    },
                    success=true
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex) 
            {
                return Json(new {success=false,message=ex.Message}, JsonRequestBehavior.AllowGet);
            }
                //return Json(new { data=emp},JsonRequestBehavior.AllowGet);            
        }



        [HttpPost]
        public ActionResult Update(Employee.Models.Entity.Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.UpdateEmployee(employee);
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
                db.DeleteEmployee(id);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }                      
        }
    }
}
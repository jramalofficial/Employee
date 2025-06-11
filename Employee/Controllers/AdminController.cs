using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employee.Models;
using Employee.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace Employee.Controllers
{
    public class AdminController : Controller
    {

        AdminDatabaseConn adminDb = new AdminDatabaseConn();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            string storedPassword=adminDb.LoginAdmin(admin.email);
            if(storedPassword!=null && adminDb.VerifyPassword(admin.password, storedPassword))
            {

                Session["Email"] = admin.email;
                return RedirectToAction("Index","Employee");
            }

            ViewBag.LoginError = "Invalid email or Password";
            return View(admin);
        }




        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Admin admin)
        {
            if (ModelState.IsValid)
            {
                if (adminDb.IsEmailExists(admin.email))
                {
                    ViewBag.LoginError = "This email already have an account";
                    return View(admin);
                }
                string hashedPassword=adminDb.HashPassword(admin.password);
                adminDb.AddAdmin(admin.username, admin.email, hashedPassword);
            }

            return View(admin);
        }



       

    }
}
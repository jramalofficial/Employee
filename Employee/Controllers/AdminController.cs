using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Employee.Models;
using Employee.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using System.Data.SqlClient;

namespace Employee.Controllers
{
   
    public class AdminController : Controller
    {

        AdminDatabaseConn adminDb = new AdminDatabaseConn();

        
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {

            if (!string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnUrl = returnUrl; 
            }

            return View();
        }
        
        [HttpPost]
        public ActionResult Login(Admin admin,string returnUrl)
        {
            string storedPassword=adminDb.LoginAdmin(admin.email);
            if(storedPassword!=null && adminDb.VerifyPassword(admin.password, storedPassword))
            {
                
                FormsAuthentication.SetAuthCookie(admin.email.ToString(),true);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl); 
                }

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
            
                if (adminDb.IsEmailExists(admin.email))
                {
                    ViewBag.LoginError = "This email already have an account";
                    return View(admin);
                }
                string hashedPassword=adminDb.HashPassword(admin.password);
                adminDb.AddAdmin(admin.username, admin.email, hashedPassword);
            
            //Session["Email"] = admin.email;
            return RedirectToAction("Index","Employee");
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ForgetPassword(string currentPassword, string newPassword)
        {

            string email = User.Identity.Name;
            if (adminDb.ChangePassword(currentPassword, newPassword, email))
            {

                TempData["Message"] = "Password changed successfully.";
                TempData["MessageType"] = "success";
                return RedirectToAction("Index", "Employee");
            }

            ViewBag.ChangePassError = "Failed to change password. Please check your current password.";
            return View();
        }


    }
}
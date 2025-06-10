using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Employee.Models.Entity
{
    public class Employee
    {

        public int? Id { get; set; }

        [Required(ErrorMessage ="First Name requried")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage ="Last Name requried")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public int DeptId { get; set; }

        [Required(ErrorMessage ="Date of Birth is Required")]
        [CustomValidation(typeof(Employee),"ValidateDob")]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage ="Email is required")]
       
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Phone number is required")]
        [RegularExpression(@"^\s*\d{10}\s*$", ErrorMessage ="Invalid Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "StreetAddress is Requried")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "City is Requried")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is Requried")]
        public string State { get; set; }


        [Required(ErrorMessage ="Country is Requried")]

        public string Country { get; set; }


        [Required(ErrorMessage = "zipCode is Requried")]
        public string ZipCode { get; set; }

        public string DepartmentName { get; set; } 


        public static ValidationResult ValidateDob(DateTime Dob,ValidationContext context)
        {
            if (Dob > DateTime.Today)
            {
                return new ValidationResult("Date of Birth cannot be in future");
            }
            return ValidationResult.Success;
        }
        

        


    }
}
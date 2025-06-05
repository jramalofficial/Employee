using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Employee.Models.Entity;

namespace Employee.Models
{
    public class DatabaseConn
    {
        

        public string ConnectionStrings = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<Employee.Models.Entity.Employee> GetAllEmployee()
        {
            List<Employee.Models.Entity.Employee> employees = new List<Employee.Models.Entity.Employee>();
            using (SqlConnection connection = new SqlConnection(ConnectionStrings))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("selectTable", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    employees.Add(new Employee.Models.Entity.Employee
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        FirstName = reader["FirstName"].ToString(),
                        MiddleName = reader["MiddleName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Department = reader["Department"].ToString()


                    });
                }
                return employees;
            }


        }
        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("selectDepartments", con))
                {

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        departments.Add(new Department
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            DepartmentName = reader["Department"].ToString()
                        });
                    }
                }
            }

            return departments;
        }
        
        public void SaveEmployee(Employee.Models.Entity.Employee employee)
        {

            using(SqlConnection con=new SqlConnection(ConnectionStrings))
            {
                using (SqlCommand cmd=new SqlCommand("addEmployee",con))
                {
                    cmd.CommandType=CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@DeptId", employee.DeptId);
                    cmd.Parameters.AddWithValue("@Dob", employee.Dob);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                    cmd.Parameters.AddWithValue("@StreetAddress", employee.StreetAddress);
                    cmd.Parameters.AddWithValue("@City", employee.City);
                    cmd.Parameters.AddWithValue("@State", employee.State);
                    cmd.Parameters.AddWithValue("@Country", employee.Country);
                    cmd.Parameters.AddWithValue("@ZipCode", employee.ZipCode);


                    con.Open();
                    cmd.ExecuteNonQuery();
                }

            }


        }










    }
}



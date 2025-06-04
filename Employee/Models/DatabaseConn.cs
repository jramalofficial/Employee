using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

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
                        DeptId = Convert.ToInt32(reader["DeptId"]),
                        Phone = reader["Phone"].ToString(),
                        Dob = reader["Dob"].ToString(),
                        Email = reader["Email"].ToString(),
                        StreetAddress = reader["StreetAddress"].ToString(),
                        City = reader["city"].ToString(),
                        State = reader["State"].ToString(),
                        Country = reader["Country"].ToString(),
                        ZipCode = reader["ZipCode"].ToString()


                    });
                }
                return employees;
            }
        }
            




   

        
        
    }
}



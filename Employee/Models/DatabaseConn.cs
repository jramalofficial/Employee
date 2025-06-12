using Employee.Models.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;



namespace Employee.Models
{
    public class DatabaseConn
    {
        

        public string ConnectionStrings = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<Employee.Models.Entity.Employee> GetAllEmployee()
        {
            List<Employee.Models.Entity.Employee> employees = new List<Employee.Models.Entity.Employee>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStrings))
                {
                    using (SqlCommand cmd = new SqlCommand("selectTable", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employees.Add(new Employee.Models.Entity.Employee
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    MiddleName = reader["MiddleName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    DepartmentName = reader["DepartmentName"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("failed to fetch employee list",ex);
            }
             return employees;
        }



        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {
                    using (SqlCommand cmd = new SqlCommand("selectDepartments", con))
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                departments.Add(new Department
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    DepartmentName = reader["DepartmentName"].ToString()
                                });
                            }
                        }
                    }
                }            
            }
            catch(SqlException ex)
            {
                throw new Exception("failed to get department", ex);
            }
            return departments;
        }
        


        public void SaveEmployee(Employee.Models.Entity.Employee employee)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {
                    using (SqlCommand cmd = new SqlCommand("addEmployee", con))
                    {                    
                        var jsonData = new
                        {
                            FirstName = employee.FirstName,
                            MiddleName = employee.MiddleName ?? (object)DBNull.Value,
                            LastName = employee.LastName,
                            DeptId = employee.DeptId,
                            Dob = employee.Dob.ToString("yyyy-MM-dd"),
                            Email = employee.Email,
                            Phone = employee.Phone,
                            StreetAddress = employee.StreetAddress,
                            City = employee.City,
                            State = employee.State,
                            Country = employee.Country,
                            ZipCode = employee.ZipCode
                        };
                        cmd.CommandType = CommandType.StoredProcedure;
                        string jsonString = new JavaScriptSerializer().Serialize(jsonData);
                        cmd.Parameters.AddWithValue("@json", jsonString);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Employee.Models.Entity.Employee Edit(int id)
        {
            Employee.Models.Entity.Employee employee = null;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {
                    using (SqlCommand cmd = new SqlCommand("selectEmployeeWithId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var jsonData = new {
                            Id=id 
                        };
                        cmd.CommandType = CommandType.StoredProcedure;
                        string jsonString = new JavaScriptSerializer().Serialize(jsonData);
                        cmd.Parameters.AddWithValue("@json", jsonString);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employee = new Employee.Models.Entity.Employee
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    MiddleName = reader["MiddleName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    DeptId = Convert.ToInt32(reader["DeptId"]),
                                    Dob = Convert.ToDateTime(reader["Dob"]),
                                    Email = reader["Email"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    StreetAddress = reader["StreetAddress"].ToString(),
                                    City = reader["City"].ToString(),
                                    State = reader["State"].ToString(),
                                    Country = reader["Country"].ToString(),
                                    ZipCode = reader["ZipCode"].ToString()
                                };
                            }
                        }
                    }

                }
            }
            catch(SqlException ex)
            {
                throw new Exception("Error occured while fetch details of employee", ex);
            }
                return employee;         
        }

        public void UpdateEmployee(Employee.Models.Entity.Employee employee)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {
                    using (SqlCommand cmd = new SqlCommand("updateEmployee", con))
                    {
                        var jsonData = new
                        {
                            Id = employee.Id,
                            FirstName = employee.FirstName,
                            MiddleName = employee.MiddleName ?? (object)DBNull.Value,
                            LastName = employee.LastName,
                            Dob = employee.Dob.ToString("yyyy-MM-dd"),
                            DeptId = employee.DeptId,
                            Email = employee.Email,
                            Phone = employee.Phone,
                            StreetAddress = employee.StreetAddress,
                            City = employee.City,
                            State = employee.State,
                            Country = employee.Country,
                            ZipCode = employee.ZipCode
                        };
                        cmd.CommandType = CommandType.StoredProcedure;
                        string jsonString = new JavaScriptSerializer().Serialize(jsonData);
                        cmd.Parameters.AddWithValue("@json", jsonString);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database Error while Updating employee", ex);
            }            
        }


        public void DeleteEmployee(int employeeId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStrings))
                {
                    using (SqlCommand cmd = new SqlCommand("deleteEmployee", con))
                    {
                        var jsonData = new
                        {
                            Id = employeeId
                        };
                        cmd.CommandType = CommandType.StoredProcedure;

                        string jsonString = new JavaScriptSerializer().Serialize(jsonData);
                        cmd.Parameters.AddWithValue("@json", jsonString);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occured while delete from database", ex);
            }
        }
    }
}



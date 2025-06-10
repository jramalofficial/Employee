using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Employee.Models.Entity;



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
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
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
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        cmd.Parameters.AddWithValue("@MiddleName", employee.MiddleName ?? (object)DBNull.Value);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

                        cmd.Parameters.AddWithValue("@Id", id);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", employee.Id);
                        cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        cmd.Parameters.AddWithValue("@MiddleName", employee.MiddleName ?? (object)DBNull.Value);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", employeeId);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }









    }
}



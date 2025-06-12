using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using System.Web.Script.Serialization;

namespace Employee.Models
{


    public class AdminDatabaseConn
    {
        public string ConnectionStrings = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public bool IsEmailExists(string email)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("ValidEmail", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    System.Diagnostics.Debug.WriteLine("Email count: " + count);

                    return count > 0;
                }
            }
        }


        public void AddAdmin(string username,string email,string password)
        {
            using (SqlConnection conn=new SqlConnection(ConnectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("insertAdmin_JSON",conn))
                {
                    var jsonData = new
                    {
                        username = username,
                        email = email,
                        password = password // already hashed before calling this
                    };
                    cmd.CommandType = CommandType.StoredProcedure;
                    string jsonString = new JavaScriptSerializer().Serialize(jsonData);
                    cmd.Parameters.AddWithValue("@json", jsonString);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string HashPassword(string password)
        {
            using (var sha256=SHA256.Create())
            {
                var bytes=Encoding.UTF8.GetBytes(password);
                var hash=sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }


        public String LoginAdmin(string email)
        {
            using(SqlConnection conn=new SqlConnection(ConnectionStrings))
            {
                using (SqlCommand cmd= new SqlCommand("getEmail",conn))
                {
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email",email);

                    conn.Open();
                    string storedPassword=cmd.ExecuteScalar()?.ToString();

                    return storedPassword;
                }

            }
        }
        public bool VerifyPassword(string inputpassword,string storedPassword)
        {
            string inputHash=HashPassword(inputpassword);
            return inputHash==storedPassword;
        }

        public bool ChangePassword(string currentPassword,string newPassword,string email) 
        {
            using (SqlConnection conn=new SqlConnection(ConnectionStrings))
            {
                using (SqlCommand cmd=new SqlCommand("getEmail",conn)) 
                { 
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);

                    conn.Open();

                   
                    string storedPassword = cmd.ExecuteScalar()?.ToString();
                    if (!VerifyPassword(currentPassword, storedPassword))
                    {
                          return false;
                    }
                    savePassword(newPassword,email);
                        
                      
                    
                }
            }
            return true;
        }

        public void savePassword(string newPassword,string email)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("updateAdminPassword", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    string Password = HashPassword(newPassword);
                    cmd.Parameters.AddWithValue("@password",Password);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
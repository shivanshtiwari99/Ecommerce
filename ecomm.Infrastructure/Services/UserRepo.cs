using Microsoft.Extensions.Configuration;
using ecomm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ecomm.Domain.Entities;
using ecomm.Application;
using Microsoft.Data.SqlClient;


namespace ecomm.Infrastructure.Services
{
    public class UserRepo:IUserServices
    {
        private readonly string _connectionString;

        public UserRepo(IConfiguration configuration) 
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public void AddUser(User_Reg user)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using SqlCommand cmd = new SqlCommand("sp_user",con) ;
                cmd.CommandType= System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", 1);
                cmd.Parameters.AddWithValue("@name", user.name);
                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.Parameters.AddWithValue("@mobile", user.mobile);
                cmd.Parameters.AddWithValue("@dob", user.dob);
                cmd.Parameters.AddWithValue("@gender", user.gender);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<Login> Login(Login login)
        {
            List<Login> logins = new List<Login>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using SqlCommand cmd = new SqlCommand("sp_login", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", 1);
                cmd.Parameters.AddWithValue("@email", login.email);
                cmd.Parameters.AddWithValue("@password", login.password);
                con.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Login log = new Login();
                    log.l_id = Convert.ToInt32(reader["l_id"]);
                    log.email = reader["email"].ToString();
                    log.password = reader["password"].ToString();
                    log.role = reader["role"].ToString();
                    logins.Add(log);  
                }
            }
            return logins;
        }
        public List<User_Reg> GetUser()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using SqlCommand cmd = new SqlCommand("sp_user", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", 2);
                con.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                List<User_Reg> list = new List<User_Reg>();
                while (reader.Read())
                {
                    User_Reg user = new User_Reg();
                    user.u_id = Convert.ToInt32(reader["u_id"]);
                    user.name = reader["name"].ToString();
                    user.email = reader["email"].ToString();
                    user.password = Convert.ToInt64(reader["password"]);
                    user.mobile = reader["mobile"].ToString();
                    user.dob = Convert.ToDateTime(reader["dob"]);
                    user.gender = reader["gender"].ToString();
                    
                    list.Add(user);
                }
                return list;
            }
        }

    }
}

using GlobalLayer;
using Microsoft.Data.SqlClient;
using Models.Employees;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Emloyees
{
    public class clsUserData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static bool DoesUserNameExist(string userName, int? excludeUserID = null)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string sql = "SELECT COUNT(*) FROM Users WHERE UserName = @UserName";
                if (excludeUserID.HasValue)
                {
                    sql += " AND UserID != @ExcludeUserID";
                }

                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserName", userName);
                if (excludeUserID.HasValue)
                    cmd.Parameters.AddWithValue("@ExcludeUserID", excludeUserID.Value);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static int InsertUser(int personID, string userName, string password, bool isActive)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_User_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PersonID", personID);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@IsActive", isActive);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError (ex);
                return -1;
            }
        }

        public static bool UpdateUser(int userID, int personID, string userName, string password, bool isActive)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_User_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@PersonID", personID);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@IsActive", isActive);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static bool DeleteUser(int userID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_User_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserID", userID);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static UserDTO GetUserByID(int userID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_User_GetByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserID", userID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new UserDTO(
                        (int)reader["UserID"],
                        (int)reader["PersonID"],
                        reader["UserName"].ToString(),
                        reader["Password"].ToString(),
                        (bool)reader["IsActive"]
                    );
                }
                return null;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return null;
            }
        }

        public static UserDTO GetUserByUserNameAndPassword(string userName, string password)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_User_GetByUserNameAndPassword", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new UserDTO(
                        (int)reader["UserID"],
                        (int)reader["PersonID"],
                        reader["UserName"].ToString(),
                        reader["Password"].ToString(),
                        (bool)reader["IsActive"]
                    );
                }
                return null;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return null;
            }
        }

        public static List<UserDTO> GetAllUsers()
        {
            List<UserDTO> list = new List<UserDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_User_GetAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new UserDTO(
                        (int)reader["UserID"],
                        (int)reader["PersonID"],
                        reader["UserName"].ToString(),
                        reader["Password"].ToString(),
                        (bool)reader["IsActive"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        public static UserDTO GetUserByUserName(string userName)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_User_GetByUsername", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserName", userName);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new UserDTO(
                        (int)reader["UserID"],
                        (int)reader["PersonID"],
                        reader["UserName"].ToString(),
                        reader["Password"].ToString(),
                        (bool)reader["IsActive"]
                    );
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetUserByUserName: " + ex.Message);
                return null;
            }
        }

        // 2. Check if User exists by UserName
        public static bool DoesUserExistByUserName(string userName)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_User_ExistsByUsername", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserName", userName);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result) == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DoesUserExistByUserName: " + ex.Message);
                return false;
            }
        }

        // 3. Check if User exists by UserName and Password
        public static bool DoesUserExistByUserNameAndPassword(string userName, string password)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_User_ExistsByUsernameAndPassword", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result) == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DoesUserExistByUserNameAndPassword: " + ex.Message);
                return false;
            }
        }
    }
}



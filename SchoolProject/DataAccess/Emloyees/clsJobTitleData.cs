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
    public class clsJobTitleData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int InsertJobTitle(string jobTitleName, string description, bool isTeaching, bool isAdministrative, bool canTeach, bool requiresCertification)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_JobTitle_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@JobTitleName", jobTitleName);
                cmd.Parameters.AddWithValue("@Description", (object)description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsTeaching", isTeaching);
                cmd.Parameters.AddWithValue("@IsAdministrative", isAdministrative);
                cmd.Parameters.AddWithValue("@CanTeach", canTeach);
                cmd.Parameters.AddWithValue("@RequiresCertification", requiresCertification);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return -1;
            }
        }

        public static bool UpdateJobTitle(int jobTitleID, string jobTitleName, string description, bool isTeaching, bool isAdministrative, bool canTeach, bool requiresCertification)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_JobTitle_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@JobTitleID", jobTitleID);
                cmd.Parameters.AddWithValue("@JobTitleName", jobTitleName);
                cmd.Parameters.AddWithValue("@Description", (object)description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsTeaching", isTeaching);
                cmd.Parameters.AddWithValue("@IsAdministrative", isAdministrative);
                cmd.Parameters.AddWithValue("@CanTeach", canTeach);
                cmd.Parameters.AddWithValue("@RequiresCertification", requiresCertification);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static int DeleteJobTitle(int jobTitleID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_JobTitle_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@JobTitleID", jobTitleID);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return -1;
            }
        }

        public static JobTitleDTO GetJobTitleByID(int jobTitleID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_JobTitle_GetByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@JobTitleID", jobTitleID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new JobTitleDTO(
                        (int)reader["JobTitleID"],
                        reader["JobTitleName"].ToString(),
                        reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                        (bool)reader["IsTeaching"],
                        (bool)reader["IsAdministrative"],
                        (bool)reader["CanTeach"],
                        (bool)reader["RequiresCertification"]
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

        public static List<JobTitleDTO> GetAllJobTitles()
        {
            List<JobTitleDTO> list = new List<JobTitleDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_JobTitle_GetAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new JobTitleDTO(
                        (int)reader["JobTitleID"],
                        reader["JobTitleName"].ToString(),
                        reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                        (bool)reader["IsTeaching"],
                        (bool)reader["IsAdministrative"],
                        (bool)reader["CanTeach"],
                        (bool)reader["RequiresCertification"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }
    }

}

using GlobalLayer;
using Microsoft.Data.SqlClient;
using Models.Rooms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Rooms
{
    public class clsClassData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int InsertClass(string className, int stageID, int capacity, string notes)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Class_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ClassName", className);
                cmd.Parameters.AddWithValue("@StageID", stageID);
                cmd.Parameters.AddWithValue("@Capacity", capacity);
                cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                //Console.WriteLine("Error in InsertClass: " + ex.Message);
                return -1;
            }
        }

        public static bool UpdateClass(int classID, string className, int stageID, int capacity, string notes)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Class_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ClassID", classID);
                cmd.Parameters.AddWithValue("@ClassName", className);
                cmd.Parameters.AddWithValue("@StageID", stageID);
                cmd.Parameters.AddWithValue("@Capacity", capacity);
                cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

                conn.Open();
                int rows = Convert.ToInt32(cmd.ExecuteScalar());
                return rows > 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                //Console.WriteLine("Error in InsertClass: " + ex.Message);
                return false;
            }
        }

        public static bool DeleteClass(int classID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Class_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ClassID", classID);

                conn.Open();

                object result = cmd.ExecuteScalar();
                int rowsDeleted = 0;

                if (result != null && int.TryParse(result.ToString(), out rowsDeleted))
                {
                    return rowsDeleted > 0;
                }
                return false;
            }
            catch (SqlException sqlEx)
            {
                clsErrorLogger.LogError(sqlEx);
                //Console.WriteLine("SQL Error in DeleteClass: " + sqlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static ClassDTO GetClassByID(int classID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Class_GetByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ClassID", classID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new ClassDTO(
                        (int)reader["ClassID"],
                        reader["ClassName"].ToString(),
                        (int)reader["StageID"],
                        (int)reader["Capacity"],
                        reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
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

        public static List<ClassDTO> GetAllClasses()
        {
            List<ClassDTO> list = new List<ClassDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Class_GetAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new ClassDTO(
                        (int)reader["ClassID"],
                        reader["ClassName"].ToString(),
                        (int)reader["StageID"],
                        (int)reader["Capacity"],
                        reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
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

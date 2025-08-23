using GlobalLayer;
using Microsoft.Data.SqlClient;
using Models.Students;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Students
{
    public class clsResultData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int studentID, int examID, decimal? score, bool isAbsent, string notes)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Result_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@ExamID", examID);
            cmd.Parameters.AddWithValue("@Score", (object)score ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsAbsent", isAbsent);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int resultID, int studentID, int examID, decimal? score, bool isAbsent, string notes)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Result_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ResultID", resultID);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@ExamID", examID);
            cmd.Parameters.AddWithValue("@Score", (object)score ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsAbsent", isAbsent);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int resultID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Result_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ResultID", resultID);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static ResultDTO GetByID(int resultID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Result_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ResultID", resultID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new ResultDTO(
                    (int)reader["ResultID"],
                    (int)reader["StudentID"],
                    (int)reader["ExamID"],
                    reader["Score"] == DBNull.Value ? null : (decimal)reader["Score"],
                    (bool)reader["IsAbsent"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                );
            }
            return null;
        }

        public static List<ResultDTO> GetByStudentID(int studentID)
        {
            List<ResultDTO> list = new List<ResultDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Result_GetByStudentID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StudentID", studentID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ResultDTO(
                    (int)reader["ResultID"],
                    (int)reader["StudentID"],
                    (int)reader["ExamID"],
                    reader["Score"] == DBNull.Value ? null : (decimal)reader["Score"],
                    (bool)reader["IsAbsent"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }

        public static List<ResultDTO> GetByExamID(int examID)
        {
            List<ResultDTO> list = new List<ResultDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Result_GetByExamID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ExamID", examID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ResultDTO(
                    (int)reader["ResultID"],
                    (int)reader["StudentID"],
                    (int)reader["ExamID"],
                    reader["Score"] == DBNull.Value ? null : (decimal)reader["Score"],
                    (bool)reader["IsAbsent"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }

        public static List<ResultDTO> GetAll()
        {
            List<ResultDTO> list = new List<ResultDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Result_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ResultDTO(
                    (int)reader["ResultID"],
                    (int)reader["StudentID"],
                    (int)reader["ExamID"],
                    reader["Score"] == DBNull.Value ? null : (decimal)reader["Score"],
                    (bool)reader["IsAbsent"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }
    }

}

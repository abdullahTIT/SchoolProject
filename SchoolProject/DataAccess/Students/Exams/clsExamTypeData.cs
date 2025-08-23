using GlobalLayer;
using Microsoft.Data.SqlClient;
using Models.Students.Exams;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Students.Exams
{
    public class clsExamTypeData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(string examTypeName, string description)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_ExamType_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ExamTypeName", examTypeName);
            cmd.Parameters.AddWithValue("@Description", (object)description ?? DBNull.Value);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int examTypeID, string examTypeName, string description)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_ExamType_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ExamTypeID", examTypeID);
            cmd.Parameters.AddWithValue("@ExamTypeName", examTypeName);
            cmd.Parameters.AddWithValue("@Description", (object)description ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int examTypeID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_ExamType_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ExamTypeID", examTypeID);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static ExamTypeDTO GetByID(int examTypeID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_ExamType_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ExamTypeID", examTypeID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new ExamTypeDTO(
                    (int)reader["ExamTypeID"],
                    reader["ExamTypeName"].ToString(),
                    reader["Description"] == DBNull.Value ? null : reader["Description"].ToString()
                );
            }
            return null;
        }

        public static List<ExamTypeDTO> GetAll()
        {
            List<ExamTypeDTO> list = new List<ExamTypeDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_ExamType_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ExamTypeDTO(
                    (int)reader["ExamTypeID"],
                    reader["ExamTypeName"].ToString(),
                    reader["Description"] == DBNull.Value ? null : reader["Description"].ToString()
                ));
            }
            return list;
        }
    }


}
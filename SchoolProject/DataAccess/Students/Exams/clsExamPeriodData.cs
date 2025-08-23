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
    public class clsExamPeriodData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(string examPeriodName, int examTypeID, int academicYearID, DateTime startDate, DateTime endDate)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_ExamPeriod_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ExamPeriodName", examPeriodName);
            cmd.Parameters.AddWithValue("@ExamTypeID", examTypeID);
            cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int examPeriodID, string examPeriodName, int examTypeID, int academicYearID, DateTime startDate, DateTime endDate)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_ExamPeriod_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ExamPeriodID", examPeriodID);
            cmd.Parameters.AddWithValue("@ExamPeriodName", examPeriodName);
            cmd.Parameters.AddWithValue("@ExamTypeID", examTypeID);
            cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int examPeriodID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_ExamPeriod_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ExamPeriodID", examPeriodID);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static ExamPeriodDTO GetByID(int examPeriodID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_ExamPeriod_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ExamPeriodID", examPeriodID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new ExamPeriodDTO(
                    (int)reader["ExamPeriodID"],
                    reader["ExamPeriodName"].ToString(),
                    (int)reader["ExamTypeID"],
                    (int)reader["AcademicYearID"],
                    (DateTime)reader["StartDate"],
                    (DateTime)reader["EndDate"]
                );
            }
            return null;
        }

        public static List<ExamPeriodDTO> GetAll()
        {
            List<ExamPeriodDTO> list = new List<ExamPeriodDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_ExamPeriod_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ExamPeriodDTO(
                    (int)reader["ExamPeriodID"],
                    reader["ExamPeriodName"].ToString(),
                    (int)reader["ExamTypeID"],
                    (int)reader["AcademicYearID"],
                    (DateTime)reader["StartDate"],
                    (DateTime)reader["EndDate"]
                ));
            }
            return list;
        }

        public static List<ExamPeriodDTO> GetByAcademicYearID(int academicYearID)
        {
            List<ExamPeriodDTO> list = new List<ExamPeriodDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_ExamPeriod_GetByAcademicYearID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ExamPeriodDTO(
                    (int)reader["ExamPeriodID"],
                    reader["ExamPeriodName"].ToString(),
                    (int)reader["ExamTypeID"],
                    (int)reader["AcademicYearID"],
                    (DateTime)reader["StartDate"],
                    (DateTime)reader["EndDate"]
                ));
            }
            return list;
        }
    }


}
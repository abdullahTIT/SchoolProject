using GlobalLayer;
using Microsoft.Data.SqlClient;
using Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.General
{
    public class clsAcademicYearData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(string academicYearName, DateTime startDate, DateTime endDate)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_AcademicYear_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AcademicYearName", academicYearName);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int academicYearID, string academicYearName, DateTime startDate, DateTime endDate)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_AcademicYear_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
            cmd.Parameters.AddWithValue("@AcademicYearName", academicYearName);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int academicYearID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_AcademicYear_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static AcademicYearDTO GetByID(int academicYearID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_AcademicYear_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new AcademicYearDTO(
                    (int)reader["AcademicYearID"],
                    reader["AcademicYearName"].ToString(),
                    (DateTime)reader["StartDate"],
                    (DateTime)reader["EndDate"]
                );
            }
            return null;
        }

        public static List<AcademicYearDTO> GetAll()
        {
            List<AcademicYearDTO> list = new List<AcademicYearDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_AcademicYear_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new AcademicYearDTO(
                    (int)reader["AcademicYearID"],
                    reader["AcademicYearName"].ToString(),
                    (DateTime)reader["StartDate"],
                    (DateTime)reader["EndDate"]
                ));
            }
            return list;
        }
    }

}

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

    public class clsStudentStageData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int studentID, int stageID, int academicYearID, DateTime enrollmentDate, bool isActive)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentStage_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@StageID", stageID);
            cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
            cmd.Parameters.AddWithValue("@EnrollmentDate", enrollmentDate);
            cmd.Parameters.AddWithValue("@IsActive", isActive);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int studentStageID, int studentID, int stageID, int academicYearID, DateTime enrollmentDate, bool isActive)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentStage_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StudentStageID", studentStageID);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@StageID", stageID);
            cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
            cmd.Parameters.AddWithValue("@EnrollmentDate", enrollmentDate);
            cmd.Parameters.AddWithValue("@IsActive", isActive);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int studentStageID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentStage_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StudentStageID", studentStageID);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static StudentStageDTO GetByID(int studentStageID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentStage_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StudentStageID", studentStageID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new StudentStageDTO(
                    (int)reader["StudentStageID"],
                    (int)reader["StudentID"],
                    (int)reader["StageID"],
                    (int)reader["AcademicYearID"],
                    (DateTime)reader["EnrollmentDate"],
                    (bool)reader["IsActive"]
                );
            }
            return null;
        }

        public static List<StudentStageDTO> GetAll()
        {
            List<StudentStageDTO> list = new List<StudentStageDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentStage_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StudentStageDTO(
                    (int)reader["StudentStageID"],
                    (int)reader["StudentID"],
                    (int)reader["StageID"],
                    (int)reader["AcademicYearID"],
                    (DateTime)reader["EnrollmentDate"],
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }

        public static List<StudentStageDTO> GetByStudentID(int studentID)
        {
            List<StudentStageDTO> list = new List<StudentStageDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentStage_GetByStudentID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StudentID", studentID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StudentStageDTO(
                    (int)reader["StudentStageID"],
                    (int)reader["StudentID"],
                    (int)reader["StageID"],
                    (int)reader["AcademicYearID"],
                    (DateTime)reader["EnrollmentDate"],
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }

        public static bool UpdateIsActive(int studentStageID, bool isActive)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentStage_UpdateIsActive", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StudentStageID", studentStageID);
            cmd.Parameters.AddWithValue("@IsActive", isActive);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static List<StudentStageDTO> GetByStageID(int stageID)
        {
            List<StudentStageDTO> list = new List<StudentStageDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentStage_GetByStageID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StageID", stageID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StudentStageDTO(
                    (int)reader["StudentStageID"],
                    (int)reader["StudentID"],
                    (int)reader["StageID"],
                    (int)reader["AcademicYearID"],
                    (DateTime)reader["EnrollmentDate"],
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }

        public static List<StudentStageDTO> GetByAcademicYearID(int academicYearID)
        {
            List<StudentStageDTO> list = new List<StudentStageDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentStage_GetByAcademicYearID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StudentStageDTO(
                    (int)reader["StudentStageID"],
                    (int)reader["StudentID"],
                    (int)reader["StageID"],
                    (int)reader["AcademicYearID"],
                    (DateTime)reader["EnrollmentDate"],
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }

    }

}


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
    public class clsExamData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int subjectID, int teacherID, DateTime examDate, DateTime takeDate, short grade, short maxGrade,
                                 string examDocument, string notes, int examPeriodID, bool isActive)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Exam_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SubjectID", subjectID);
            cmd.Parameters.AddWithValue("@TeacherID", teacherID);
            cmd.Parameters.AddWithValue("@ExamDate", examDate);
            cmd.Parameters.AddWithValue("@TakeDate", takeDate);
            cmd.Parameters.AddWithValue("@Grade", grade);
            cmd.Parameters.AddWithValue("@MaxGrade", maxGrade);
            cmd.Parameters.AddWithValue("@ExamDocument", (object)examDocument ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ExamPeriodID", examPeriodID);
            cmd.Parameters.AddWithValue("@IsActive", isActive);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int examID, int subjectID, int teacherID, DateTime examDate, DateTime takeDate, short grade, short maxGrade,
                                  string examDocument, string notes, int examPeriodID, bool isActive)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Exam_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ExamID", examID);
            cmd.Parameters.AddWithValue("@SubjectID", subjectID);
            cmd.Parameters.AddWithValue("@TeacherID", teacherID);
            cmd.Parameters.AddWithValue("@ExamDate", examDate);
            cmd.Parameters.AddWithValue("@TakeDate", takeDate);
            cmd.Parameters.AddWithValue("@Grade", grade);
            cmd.Parameters.AddWithValue("@MaxGrade", maxGrade);
            cmd.Parameters.AddWithValue("@ExamDocument", (object)examDocument ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ExamPeriodID", examPeriodID);
            cmd.Parameters.AddWithValue("@IsActive", isActive);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int examID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Exam_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ExamID", examID);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static ExamDTO GetByID(int examID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Exam_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ExamID", examID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new ExamDTO(
                    (int)reader["ExamID"],
                    (int)reader["SubjectID"],
                    (int)reader["TeacherID"],
                    (DateTime)reader["ExamDate"],
                    (DateTime)reader["TakeDate"],
                    (short)reader["Grade"],
                    (short)reader["MaxGrade"],
                    reader["ExamDocument"] == DBNull.Value ? null : reader["ExamDocument"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                    (int)reader["ExamPeriodID"],
                    (bool)reader["IsActive"]
                );
            }
            return null;
        }

        public static List<ExamDTO> GetAll()
        {
            List<ExamDTO> list = new List<ExamDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Exam_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ExamDTO(
                    (int)reader["ExamID"],
                    (int)reader["SubjectID"],
                    (int)reader["TeacherID"],
                    (DateTime)reader["ExamDate"],
                    (DateTime)reader["TakeDate"],
                    (short)reader["Grade"],
                    (short)reader["MaxGrade"],
                    reader["ExamDocument"] == DBNull.Value ? null : reader["ExamDocument"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                    (int)reader["ExamPeriodID"],
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }

        public static List<ExamDTO> GetActive()
        {
            List<ExamDTO> list = new List<ExamDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Exam_GetActive", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ExamDTO(
                    (int)reader["ExamID"],
                    (int)reader["SubjectID"],
                    (int)reader["TeacherID"],
                    (DateTime)reader["ExamDate"],
                    (DateTime)reader["TakeDate"],
                    (short)reader["Grade"],
                    (short)reader["MaxGrade"],
                    reader["ExamDocument"] == DBNull.Value ? null : reader["ExamDocument"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                    (int)reader["ExamPeriodID"],
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }

        public static List<ExamDTO> GetBySubjectID(int subjectID)
        {
            List<ExamDTO> list = new List<ExamDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Exam_GetBySubjectID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SubjectID", subjectID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ExamDTO(
                    (int)reader["ExamID"],
                    (int)reader["SubjectID"],
                    (int)reader["TeacherID"],
                    (DateTime)reader["ExamDate"],
                    (DateTime)reader["TakeDate"],
                    (short)reader["Grade"],
                    (short)reader["MaxGrade"],
                    reader["ExamDocument"] == DBNull.Value ? null : reader["ExamDocument"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                    (int)reader["ExamPeriodID"],
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }
    }


}

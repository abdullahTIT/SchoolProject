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
    public class clsFinalGradesData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static FinalGradeDTO GetByID(int finalGradeID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_FinalGrade_GetByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@FinalGradeID", finalGradeID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new FinalGradeDTO(
                        (int)reader["FinalGradeID"],
                        (int)reader["StudentID"],
                        (int)reader["SubjectID"],
                        (int)reader["StageID"],
                        (int)reader["AcademicYearID"],
                        (int)reader["ExamPeriodID"],
                        (int)reader["ExamTypeID"],
                        (decimal)reader["Grade"],
                        (decimal)reader["MaxGrade"],
                        (DateTime)reader["CalculationDate"],
                        reader["Notes"]?.ToString()
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

        public static List<FinalGradeDTO> GetAll()
        {
            List<FinalGradeDTO> list = new List<FinalGradeDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_FinalGrade_GetAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new FinalGradeDTO(
                        (int)reader["FinalGradeID"],
                        (int)reader["StudentID"],
                        (int)reader["SubjectID"],
                        (int)reader["StageID"],
                        (int)reader["AcademicYearID"],
                        (int)reader["ExamPeriodID"],
                        (int)reader["ExamTypeID"],
                        (decimal)reader["Grade"],
                        (decimal)reader["MaxGrade"],
                        (DateTime)reader["CalculationDate"],
                        reader["Notes"]?.ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        public static FinalGradeDTO? GetFinalGrade(int studentID, int subjectID, int stageID, int academicYearID, int examPeriodID, int examTypeID = 1)
        {
            FinalGradeDTO? result = null;

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);

                using SqlCommand cmd = new SqlCommand("sp_GetFinalGrade", conn);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@SubjectID", subjectID);
                cmd.Parameters.AddWithValue("@StageID", stageID);
                cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
                cmd.Parameters.AddWithValue("@ExamPeriodID", examPeriodID);
                cmd.Parameters.AddWithValue("@ExamTypeID", examTypeID);

                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = new FinalGradeDTO
                    {
                        StudentID = (int)reader["StudentID"],
                        SubjectID = (int)reader["SubjectID"],
                        StageID = (int)reader["StageID"],
                        AcademicYearID = (int)reader["AcademicYearID"],
                        ExamPeriodID = (int)reader["ExamPeriodID"],
                        ExamTypeID = (int)reader["ExamTypeID"],
                        Grade = (decimal)reader["Grade"]
                    };
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return result;
        }

        public static List<FinalGradeDTO> GetByStudentID(int studentID)
        {
            List<FinalGradeDTO> results = new List<FinalGradeDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_FinalGrade_GetByStudentID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@StudentID", studentID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new FinalGradeDTO(
                        (int)reader["FinalGradeID"],
                        (int)reader["StudentID"],
                        (int)reader["SubjectID"],
                        (int)reader["StageID"],
                        (int)reader["AcademicYearID"],
                        (int)reader["ExamPeriodID"],
                        (int)reader["ExamTypeID"],
                        (decimal)reader["Grade"],
                        (decimal)reader["MaxGrade"],
                        (DateTime)reader["CalculationDate"],
                        reader["Notes"]?.ToString()
                    ));
                }
                return results;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return results;  // فارغة إذا حصل خطأ
            }
        }

        public static List<FinalGradeDTO> GetBySubjectID(int subjectID)
        {
            List<FinalGradeDTO> results = new List<FinalGradeDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_FinalGrade_GetBySubjectID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@SubjectID", subjectID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new FinalGradeDTO(
                        (int)reader["FinalGradeID"],
                        (int)reader["StudentID"],
                        (int)reader["SubjectID"],
                        (int)reader["StageID"],
                        (int)reader["AcademicYearID"],
                        (int)reader["ExamPeriodID"],
                        (int)reader["ExamTypeID"],
                        (decimal)reader["Grade"],
                        (decimal)reader["MaxGrade"],
                        (DateTime)reader["CalculationDate"],
                        reader["Notes"]?.ToString()
                    ));
                }
                return results;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return results;  // فارغة إذا حصل خطأ
            }
        }

        public static bool CalculateWeeklyGrade(int studentID, int subjectID, int stageID, int academicYearID, int examPeriodID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_CalculateWeeklyGrade", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@SubjectID", subjectID);
                cmd.Parameters.AddWithValue("@StageID", stageID);
                cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
                cmd.Parameters.AddWithValue("@ExamPeriodID", examPeriodID);

                conn.Open();
                cmd.ExecuteNonQuery();

                return true; // نفذ بنجاح
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false; // حدث خطأ
            }
        }
        public static bool CalculateMonthlyGrade(int studentID, int subjectID, int stageID, int academicYearID, int examPeriodID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_CalculateMonthlyGrade", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@SubjectID", subjectID);
                cmd.Parameters.AddWithValue("@StageID", stageID);
                cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
                cmd.Parameters.AddWithValue("@ExamPeriodID", examPeriodID);

                conn.Open();
                cmd.ExecuteNonQuery();

                return true; // تم التنفيذ بنجاح
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false; // حدث خطأ
            }
        }
        public static bool CalculateTermGrade(int studentID, int subjectID, int stageID, int academicYearID, int examPeriodID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_CalculateTermGrade", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@SubjectID", subjectID);
                cmd.Parameters.AddWithValue("@StageID", stageID);
                cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
                cmd.Parameters.AddWithValue("@ExamPeriodID", examPeriodID);

                conn.Open();
                cmd.ExecuteNonQuery();

                return true; // التنفيذ ناجح
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false; // حدث خطأ
            }
        }
        public static bool CalculateYearlyGrade(int studentID, int subjectID, int stageID, int academicYearID, int examPeriodID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_CalculateYearlyGrade", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@SubjectID", subjectID);
                cmd.Parameters.AddWithValue("@StageID", stageID);
                cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
                cmd.Parameters.AddWithValue("@ExamPeriodID", examPeriodID);

                conn.Open();
                cmd.ExecuteNonQuery();

                return true; // التنفيذ ناجح
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false; // حدث خطأ
            }
        }
        public static bool CalculateAllGradesForStudent(int studentID, int subjectID, int stageID, int academicYearID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_CalculateAllGradesForStudent", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@SubjectID", subjectID);
                cmd.Parameters.AddWithValue("@StageID", stageID);
                cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);

                conn.Open();
                cmd.ExecuteNonQuery();

                return true; // التنفيذ ناجح
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false; // حدث خطأ
            }
        }
        public static bool CalculateAllGradesForAllStudentsAllSubjects()
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_CalculateAllGrades_AllStudents_AllSubjects", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                cmd.ExecuteNonQuery();

                return true; // التنفيذ ناجح
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false; // حدث خطأ
            }
        }

    }

}
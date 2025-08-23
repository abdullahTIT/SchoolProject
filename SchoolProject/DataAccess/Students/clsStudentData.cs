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
    public class clsStudentData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int InsertStudent(int personID, int guardianID, DateTime enrollmentDate, int stageID,
                                        bool isActive, string documentPath, string notes)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Student_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PersonID", personID);
                cmd.Parameters.AddWithValue("@GuardianID", guardianID);
                cmd.Parameters.AddWithValue("@EnrollmentDate", enrollmentDate);
                cmd.Parameters.AddWithValue("@StageID", stageID);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@DocumentPath", (object)documentPath ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

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

        public static bool UpdateStudent(int studentID, int personID, int guardianID, DateTime enrollmentDate, int stageID,
                                         bool isActive, string documentPath, string notes)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Student_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@PersonID", personID);
                cmd.Parameters.AddWithValue("@GuardianID", guardianID);
                cmd.Parameters.AddWithValue("@EnrollmentDate", enrollmentDate);
                cmd.Parameters.AddWithValue("@StageID", stageID);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@DocumentPath", (object)documentPath ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

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

        public static bool DeleteStudent(int studentID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Student_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StudentID", studentID);

                conn.Open();
                object result = cmd.ExecuteScalar();
                int rowsAffected = 0;
                if (result != null && int.TryParse(result.ToString(), out rowsAffected))
                    return rowsAffected > 0;

                return false;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static StudentDTO GetStudentByID(int studentID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Student_GetByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StudentID", studentID);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new StudentDTO(
                        (int)reader["StudentID"],
                        (int)reader["PersonID"],
                        (int)reader["GuardianID"],
                        (DateTime)reader["EnrollmentDate"],
                        (int)reader["StageID"],
                        (bool)reader["IsActive"],
                        reader["DocumentPath"] == DBNull.Value ? null : reader["DocumentPath"].ToString(),
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

        public static List<StudentDTO> GetAllStudents()
        {
            List<StudentDTO> list = new List<StudentDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Student_GetAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StudentDTO(
                        (int)reader["StudentID"],
                        (int)reader["PersonID"],
                        (int)reader["GuardianID"],
                        (DateTime)reader["EnrollmentDate"],
                        (int)reader["StageID"],
                        (bool)reader["IsActive"],
                        reader["DocumentPath"] == DBNull.Value ? null : reader["DocumentPath"].ToString(),
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

        public static bool UpdateStudentIsActive(int studentID, bool isActive)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Student_UpdateIsActive", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@IsActive", isActive);

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

        public static List<StudentDTO> GetStudentsByStage(int stageID)
        {
            List<StudentDTO> list = new List<StudentDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Student_GetByStage", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@StageID", stageID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StudentDTO(
                        (int)reader["StudentID"],
                        (int)reader["PersonID"],
                        (int)reader["GuardianID"],
                        (DateTime)reader["EnrollmentDate"],
                        (int)reader["StageID"],
                        (bool)reader["IsActive"],
                        reader["DocumentPath"] == DBNull.Value ? null : reader["DocumentPath"].ToString(),
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

        public static List<StudentDTO> GetStudentsByGuardianID(int guardianID)
        {
            List<StudentDTO> list = new List<StudentDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Student_GetByGuardianID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@GuardianID", guardianID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StudentDTO(
                        (int)reader["StudentID"],
                        (int)reader["PersonID"],
                        (int)reader["GuardianID"],
                        (DateTime)reader["EnrollmentDate"],
                        (int)reader["StageID"],
                        (bool)reader["IsActive"],
                        reader["DocumentPath"] == DBNull.Value ? null : reader["DocumentPath"].ToString(),
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

        public static int GetStudentsEnrollmentCount()
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetStudentsEnrollmentCount", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            return (int)cmd.ExecuteScalar();
        }
        public static List<StudentDTO> GetStudentsEnrollment()
        {
            List<StudentDTO> list = new List<StudentDTO>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetStudentsEnrollment", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StudentDTO(
                    (int)reader["StudentID"],
                    (int)reader["PersonID"],
                    0, // GuardianID غير موجود في الاستعلام هنا
                    (DateTime)reader["EnrollmentDate"],
                    (int)reader["StageID"],
                    (bool)reader["IsActive"],
                    null, // DocumentPath غير موجود هنا
                    null  // Notes غير موجود هنا
                ));
            }
            return list;
        }
        public static int GetStudentsEnrollmentCountInStage(int stageID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetStudentsEnrollmentCountInStage", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StageID", stageID);

            conn.Open();
            return (int)cmd.ExecuteScalar();
        }

        // 4. قائمة الطلاب المسجلين في مرحلة معينة مع بياناتهم الأساسية
        public static List<StudentDTO> GetStudentsEnrollmentInStage(int stageID)
        {
            List<StudentDTO> list = new List<StudentDTO>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetStudentsEnrollmentInStage", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StageID", stageID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StudentDTO(
                    (int)reader["StudentID"],
                    (int)reader["PersonID"],
                    0, // GuardianID غير موجود هنا
                    (DateTime)reader["EnrollmentDate"],
                    (int)reader["StageID"],
                    (bool)reader["IsActive"],
                    null,
                    null
                ));
            }
            return list;
        }
        public static int GetStudentsEnrollmentCountByDate(DateTime startDate, DateTime endDate)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetStudentsEnrollmentCountByDate", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            conn.Open();
            return (int)cmd.ExecuteScalar();
        }

        public static List<StudentDTO> GetStudentsEnrollmentByDate(DateTime startDate, DateTime endDate)
        {
            List<StudentDTO> list = new List<StudentDTO>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetStudentsEnrollmentByDate", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StudentDTO(
                    (int)reader["StudentID"],
                    (int)reader["PersonID"],
                    0,
                    (DateTime)reader["EnrollmentDate"],
                    (int)reader["StageID"],
                    (bool)reader["IsActive"],
                    null,
                    null
                ));
            }
            return list;
        }

        public static int GetStudentsEnrollmentCountInStageByDate(int stageID, DateTime startDate, DateTime endDate)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetStudentsEnrollmentCountInStageByDate", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StageID", stageID);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            conn.Open();
            return (int)cmd.ExecuteScalar();
        }

        public static List<StudentDTO> GetStudentsEnrollmentInStageByDate(int stageID, DateTime startDate, DateTime endDate)
        {
            List<StudentDTO> list = new List<StudentDTO>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetStudentsEnrollmentInStageByDate", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StageID", stageID);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StudentDTO(
                    (int)reader["StudentID"],
                    (int)reader["PersonID"],
                    0,
                    (DateTime)reader["EnrollmentDate"],
                    (int)reader["StageID"],
                    (bool)reader["IsActive"],
                    null,
                    null
                ));
            }
            return list;
        }
    }
}
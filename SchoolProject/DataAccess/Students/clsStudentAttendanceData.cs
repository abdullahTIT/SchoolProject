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

    public class clsStudentAttendanceData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int studentID, int scheduleID, DateTime attendanceDate, string status, string notes)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentAttendance_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@ScheduleID", scheduleID);
                cmd.Parameters.AddWithValue("@AttendanceDate", attendanceDate);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                clsErrorLogger.LogError(ex);
                return -1;
            }
        }

        public static bool Update(int attendanceID, int studentID, int scheduleID, DateTime attendanceDate, string status, string notes)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentAttendance_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@ScheduleID", scheduleID);
                cmd.Parameters.AddWithValue("@AttendanceDate", attendanceDate);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static bool Delete(int attendanceID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentAttendance_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);

                conn.Open();
                int rows = Convert.ToInt32(cmd.ExecuteScalar());
                return rows > 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static StudentAttendanceDTO GetByID(int attendanceID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentAttendance_GetByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new StudentAttendanceDTO(
                        (int)reader["AttendanceID"],
                        (int)reader["StudentID"],
                        (int)reader["ScheduleID"],
                        (DateTime)reader["AttendanceDate"],
                        reader["Status"].ToString(),
                        reader["Notes"]?.ToString()
                    );
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return null;
        }

        public static List<StudentAttendanceDTO> GetAll()
        {
            List<StudentAttendanceDTO> list = new List<StudentAttendanceDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentAttendance_GetAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StudentAttendanceDTO(
                        (int)reader["AttendanceID"],
                        (int)reader["StudentID"],
                        (int)reader["ScheduleID"],
                        (DateTime)reader["AttendanceDate"],
                        reader["Status"].ToString(),
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

        public static List<StudentAttendanceDTO> GetByStudentID(int studentID)
        {
            List<StudentAttendanceDTO> list = new List<StudentAttendanceDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentAttendance_GetByStudentID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@StudentID", studentID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StudentAttendanceDTO(
                        (int)reader["AttendanceID"],
                        (int)reader["StudentID"],
                        (int)reader["ScheduleID"],
                        (DateTime)reader["AttendanceDate"],
                        reader["Status"].ToString(),
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

        public static List<StudentAttendanceDTO> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            List<StudentAttendanceDTO> list = new List<StudentAttendanceDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentAttendance_GetByDateRange", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StudentAttendanceDTO(
                        (int)reader["AttendanceID"],
                        (int)reader["StudentID"],
                        (int)reader["ScheduleID"],
                        (DateTime)reader["AttendanceDate"],
                        reader["Status"].ToString(),
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

        public static List<StudentAttendanceDTO> GetByStatus(string status)
        {
            List<StudentAttendanceDTO> list = new List<StudentAttendanceDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentAttendance_GetByStatus", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Status", status);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StudentAttendanceDTO(
                        (int)reader["AttendanceID"],
                        (int)reader["StudentID"],
                        (int)reader["ScheduleID"],
                        (DateTime)reader["AttendanceDate"],
                        reader["Status"].ToString(),
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

        public static bool DoesAttendanceExist(int studentID, int scheduleID, DateTime attendanceDate)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentAttendance_Exists", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@ScheduleID", scheduleID);
                cmd.Parameters.AddWithValue("@AttendanceDate", attendanceDate);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null && Convert.ToInt32(result) == 1;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return true; // احتياطيًا منع الإضافة في حالة الخطأ
            }
        }

        public static bool IsAttendanceRelated(int attendanceID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentAttendance_IsRelated", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);

                SqlParameter outputParam = new SqlParameter("@IsRelated", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return Convert.ToBoolean(outputParam.Value);
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return true; // افتراضياً لمنع الحذف عند الخطأ
            }


        }

    }


}

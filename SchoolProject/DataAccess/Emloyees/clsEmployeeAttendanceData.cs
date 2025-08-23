using GlobalLayer;
using Microsoft.Data.SqlClient;
using Models.Employees;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Emloyees
{
    public class clsEmployeeAttendanceData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int InsertAttendance(int employeeID, DateTime attendanceDate, TimeSpan? checkInTime, TimeSpan? checkOutTime, string status)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_EmployeeAttendance_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@AttendanceDate", attendanceDate);
                cmd.Parameters.AddWithValue("@CheckInTime", (object)checkInTime ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CheckOutTime", (object)checkOutTime ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", status);

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

        public static bool UpdateAttendance(int attendanceID, int employeeID, DateTime attendanceDate, TimeSpan? checkInTime, TimeSpan? checkOutTime, string status)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_EmployeeAttendance_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@AttendanceDate", attendanceDate);
                cmd.Parameters.AddWithValue("@CheckInTime", (object)checkInTime ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CheckOutTime", (object)checkOutTime ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", status);

                conn.Open();

                // ExecuteScalar لالتقاط قيمة RowsAffected
                var result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int rowsAffected))
                {
                    return rowsAffected > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static bool DeleteAttendance(int attendanceID, out int rowsAffected)
        {
            rowsAffected = 0;
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_EmployeeAttendance_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    rowsAffected = Convert.ToInt32(reader["RowsAffected"]);
                }

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static EmployeeAttendanceDTO GetAttendanceByID(int attendanceID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_EmployeeAttendance_GetByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AttendanceID", attendanceID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new EmployeeAttendanceDTO(
                        (int)reader["AttendanceID"],
                        (int)reader["EmployeeID"],
                        (DateTime)reader["AttendanceDate"],
                        reader["CheckInTime"] == DBNull.Value ? null : (TimeSpan)reader["CheckInTime"],
                        reader["CheckOutTime"] == DBNull.Value ? null : (TimeSpan)reader["CheckOutTime"],
                        reader["Status"].ToString()
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

        public static List<EmployeeAttendanceDTO> GetAllAttendances()
        {
            List<EmployeeAttendanceDTO> list = new List<EmployeeAttendanceDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_EmployeeAttendance_GetAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new EmployeeAttendanceDTO(
                        (int)reader["AttendanceID"],
                        (int)reader["EmployeeID"],
                        (DateTime)reader["AttendanceDate"],
                        reader["CheckInTime"] == DBNull.Value ? null : (TimeSpan)reader["CheckInTime"],
                        reader["CheckOutTime"] == DBNull.Value ? null : (TimeSpan)reader["CheckOutTime"],
                        reader["Status"].ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        public static bool IsAttendanceExists(int employeeID, DateTime attendanceDate)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_EmployeeAttendance_Exists", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@AttendanceDate", attendanceDate);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result) == 1;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static List<EmployeeAttendanceDTO> GetAttendanceByEmployeeID(int employeeID)
        {
            List<EmployeeAttendanceDTO> list = new List<EmployeeAttendanceDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_EmployeeAttendance_GetByEmployeeID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new EmployeeAttendanceDTO(
                        (int)reader["AttendanceID"],
                        (int)reader["EmployeeID"],
                        (DateTime)reader["AttendanceDate"],
                        reader["CheckInTime"] == DBNull.Value ? null : (TimeSpan)reader["CheckInTime"],
                        reader["CheckOutTime"] == DBNull.Value ? null : (TimeSpan)reader["CheckOutTime"],
                        reader["Status"].ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        public static List<EmployeeAttendanceDTO> GetAttendanceByEmployeeIDBetweenDates(int employeeID, DateTime startDate, DateTime endDate)
        {
            List<EmployeeAttendanceDTO> list = new List<EmployeeAttendanceDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_EmployeeAttendance_GetByEmployeeIDBetweenDates", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new EmployeeAttendanceDTO(
                        (int)reader["AttendanceID"],
                        (int)reader["EmployeeID"],
                        (DateTime)reader["AttendanceDate"],
                        reader["CheckInTime"] == DBNull.Value ? null : (TimeSpan)reader["CheckInTime"],
                        reader["CheckOutTime"] == DBNull.Value ? null : (TimeSpan)reader["CheckOutTime"],
                        reader["Status"].ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        public static List<EmployeeAttendanceDTO> GetAttendanceByStatus(string status)
        {
            List<EmployeeAttendanceDTO> list = new List<EmployeeAttendanceDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_EmployeeAttendance_GetByStatus", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Status", status);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new EmployeeAttendanceDTO(
                        (int)reader["AttendanceID"],
                        (int)reader["EmployeeID"],
                        (DateTime)reader["AttendanceDate"],
                        reader["CheckInTime"] == DBNull.Value ? null : (TimeSpan)reader["CheckInTime"],
                        reader["CheckOutTime"] == DBNull.Value ? null : (TimeSpan)reader["CheckOutTime"],
                        reader["Status"].ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        public static List<EmployeeAttendanceDTO> GetAttendanceByStatusBetweenDates(string status, DateTime startDate, DateTime endDate)
        {
            List<EmployeeAttendanceDTO> list = new List<EmployeeAttendanceDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_EmployeeAttendance_GetByStatusBetweenDates", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new EmployeeAttendanceDTO(
                        (int)reader["AttendanceID"],
                        (int)reader["EmployeeID"],
                        (DateTime)reader["AttendanceDate"],
                        reader["CheckInTime"] == DBNull.Value ? null : (TimeSpan)reader["CheckInTime"],
                        reader["CheckOutTime"] == DBNull.Value ? null : (TimeSpan)reader["CheckOutTime"],
                        reader["Status"].ToString()
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

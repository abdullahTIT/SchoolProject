using GlobalLayer;
using Microsoft.Data.SqlClient;
using Models.Rooms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Rooms
{
    public class clsBookingData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int roomID, int employeeID, string purpose, DateTime bookingDate, DateTime startTime, DateTime endTime,
                                 string status, string notes, string recurrenceType, DateTime? recurrenceEndDate)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Booking_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@RoomID", roomID);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@Purpose", purpose);
            cmd.Parameters.AddWithValue("@BookingDate", bookingDate);
            cmd.Parameters.AddWithValue("@StartTime", startTime);
            cmd.Parameters.AddWithValue("@EndTime", endTime);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RecurrenceType", (object)recurrenceType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RecurrenceEndDate", (object)recurrenceEndDate ?? DBNull.Value);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int bookingID, int roomID, int employeeID, string purpose, DateTime bookingDate, DateTime startTime, DateTime endTime,
                                  string status, string notes, string recurrenceType, DateTime? recurrenceEndDate)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Booking_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@BookingID", bookingID);
            cmd.Parameters.AddWithValue("@RoomID", roomID);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@Purpose", purpose);
            cmd.Parameters.AddWithValue("@BookingDate", bookingDate);
            cmd.Parameters.AddWithValue("@StartTime", startTime);
            cmd.Parameters.AddWithValue("@EndTime", endTime);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RecurrenceType", (object)recurrenceType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RecurrenceEndDate", (object)recurrenceEndDate ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int bookingID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Booking_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@BookingID", bookingID);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static BookingDTO GetByID(int bookingID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Booking_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@BookingID", bookingID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new BookingDTO(
                    (int)reader["BookingID"],
                    (int)reader["RoomID"],
                    (int)reader["EmployeeID"],
                    reader["Purpose"].ToString(),
                    (DateTime)reader["BookingDate"],
                    (DateTime)reader["StartTime"],
                    (DateTime)reader["EndTime"],
                    reader["Status"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                    reader["RecurrenceType"] == DBNull.Value ? null : reader["RecurrenceType"].ToString(),
                    reader["RecurrenceEndDate"] == DBNull.Value ? null : (DateTime)reader["RecurrenceEndDate"]
                );
            }
            return null;
        }

        public static List<BookingDTO> GetAll()
        {
            List<BookingDTO> list = new List<BookingDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Booking_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new BookingDTO(
                    (int)reader["BookingID"],
                    (int)reader["RoomID"],
                    (int)reader["EmployeeID"],
                    reader["Purpose"].ToString(),
                    (DateTime)reader["BookingDate"],
                    (DateTime)reader["StartTime"],
                    (DateTime)reader["EndTime"],
                    reader["Status"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                    reader["RecurrenceType"] == DBNull.Value ? null : reader["RecurrenceType"].ToString(),
                    reader["RecurrenceEndDate"] == DBNull.Value ? null : (DateTime)reader["RecurrenceEndDate"]
                ));
            }
            return list;
        }

        public static List<BookingDTO> GetByRoomID(int roomID)
        {
            List<BookingDTO> list = new List<BookingDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Booking_GetByRoomID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@RoomID", roomID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new BookingDTO(
                    (int)reader["BookingID"],
                    (int)reader["RoomID"],
                    (int)reader["EmployeeID"],
                    reader["Purpose"].ToString(),
                    (DateTime)reader["BookingDate"],
                    (DateTime)reader["StartTime"],
                    (DateTime)reader["EndTime"],
                    reader["Status"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                    reader["RecurrenceType"] == DBNull.Value ? null : reader["RecurrenceType"].ToString(),
                    reader["RecurrenceEndDate"] == DBNull.Value ? null : (DateTime)reader["RecurrenceEndDate"]
                ));
            }
            return list;
        }

        public static List<BookingDTO> GetByEmployeeID(int employeeID)
        {
            List<BookingDTO> list = new List<BookingDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Booking_GetByEmployeeID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new BookingDTO(
                    (int)reader["BookingID"],
                    (int)reader["RoomID"],
                    (int)reader["EmployeeID"],
                    reader["Purpose"].ToString(),
                    (DateTime)reader["BookingDate"],
                    (DateTime)reader["StartTime"],
                    (DateTime)reader["EndTime"],
                    reader["Status"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                    reader["RecurrenceType"] == DBNull.Value ? null : reader["RecurrenceType"].ToString(),
                    reader["RecurrenceEndDate"] == DBNull.Value ? null : (DateTime)reader["RecurrenceEndDate"]
                ));
            }
            return list;
        }
    }

}

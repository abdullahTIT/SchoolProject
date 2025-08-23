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
    public class clsRoomData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(string roomName, int roomTypeID, int capacity, string location, bool isAvailable, bool isReservable, string notes)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Room_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@RoomName", roomName);
            cmd.Parameters.AddWithValue("@RoomTypeID", roomTypeID);
            cmd.Parameters.AddWithValue("@Capacity", capacity);
            cmd.Parameters.AddWithValue("@Location", location);
            cmd.Parameters.AddWithValue("@IsAvailable", isAvailable);
            cmd.Parameters.AddWithValue("@IsReservable", isReservable);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int roomID, string roomName, int roomTypeID, int capacity, string location, bool isAvailable, bool isReservable, string notes)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Room_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@RoomID", roomID);
            cmd.Parameters.AddWithValue("@RoomName", roomName);
            cmd.Parameters.AddWithValue("@RoomTypeID", roomTypeID);
            cmd.Parameters.AddWithValue("@Capacity", capacity);
            cmd.Parameters.AddWithValue("@Location", location);
            cmd.Parameters.AddWithValue("@IsAvailable", isAvailable);
            cmd.Parameters.AddWithValue("@IsReservable", isReservable);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int roomID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Room_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@RoomID", roomID);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static RoomDTO GetByID(int roomID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Room_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@RoomID", roomID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new RoomDTO(
                    (int)reader["RoomID"],
                    reader["RoomName"].ToString(),
                    (int)reader["RoomTypeID"],
                    (int)reader["Capacity"],
                    reader["Location"].ToString(),
                    (bool)reader["IsAvailable"],
                    (bool)reader["IsReservable"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                );
            }
            return null;
        }

        public static List<RoomDTO> GetAll()
        {
            List<RoomDTO> list = new List<RoomDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Room_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new RoomDTO(
                    (int)reader["RoomID"],
                    reader["RoomName"].ToString(),
                    (int)reader["RoomTypeID"],
                    (int)reader["Capacity"],
                    reader["Location"].ToString(),
                    (bool)reader["IsAvailable"],
                    (bool)reader["IsReservable"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }

        public static List<RoomDTO> GetAvailableRooms()
        {
            List<RoomDTO> list = new List<RoomDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Room_GetAvailable", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new RoomDTO(
                    (int)reader["RoomID"],
                    reader["RoomName"].ToString(),
                    (int)reader["RoomTypeID"],
                    (int)reader["Capacity"],
                    reader["Location"].ToString(),
                    (bool)reader["IsAvailable"],
                    (bool)reader["IsReservable"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }

        public static List<RoomDTO> GetAvailableRoomsAtTime(DateTime targetDateTime)
        {
            List<RoomDTO> list = new List<RoomDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("ps_GetAvailableRoomsAtTime", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            // تحويل اليوم إلى اسم اليوم الكامل بالإنجليزي
            string dayName = targetDateTime.ToString("dddd", System.Globalization.CultureInfo.InvariantCulture);

            // استخراج الوقت فقط
            TimeSpan timeOfDay = targetDateTime.TimeOfDay;

            // تمرير الباراميترات
            cmd.Parameters.AddWithValue("@CheckDay", dayName);
            cmd.Parameters.AddWithValue("@CheckTime", timeOfDay);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new RoomDTO(
                    (int)reader["RoomID"],
                    reader["RoomName"].ToString(),
                    (int)reader["RoomTypeID"],
                    (int)reader["Capacity"],
                    reader["Location"].ToString(),
                    (bool)reader["IsAvailable"],
                    (bool)reader["IsReservable"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }

    }

}

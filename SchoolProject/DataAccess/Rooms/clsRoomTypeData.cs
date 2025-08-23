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
    public class clsRoomTypeData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(string typeName, string description, bool isActive)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_RoomType_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@TypeName", typeName);
            cmd.Parameters.AddWithValue("@Description", (object)description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsActive", isActive);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int roomTypeID, string typeName, string description, bool isActive)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_RoomType_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@RoomTypeID", roomTypeID);
            cmd.Parameters.AddWithValue("@TypeName", typeName);
            cmd.Parameters.AddWithValue("@Description", (object)description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsActive", isActive);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int roomTypeID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_RoomType_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@RoomTypeID", roomTypeID);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static RoomTypeDTO GetByID(int roomTypeID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_RoomType_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@RoomTypeID", roomTypeID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new RoomTypeDTO(
                    (int)reader["RoomTypeID"],
                    reader["TypeName"].ToString(),
                    reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                    (bool)reader["IsActive"]
                );
            }
            return null;
        }

        public static List<RoomTypeDTO> GetAll()
        {
            List<RoomTypeDTO> list = new List<RoomTypeDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_RoomType_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new RoomTypeDTO(
                    (int)reader["RoomTypeID"],
                    reader["TypeName"].ToString(),
                    reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }

        public static List<RoomTypeDTO> GetActive()
        {
            List<RoomTypeDTO> list = new List<RoomTypeDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_RoomType_GetActive", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new RoomTypeDTO(
                    (int)reader["RoomTypeID"],
                    reader["TypeName"].ToString(),
                    reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }
    }

}
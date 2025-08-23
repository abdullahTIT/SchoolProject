using GlobalLayer;
using Microsoft.Data.SqlClient;
using Models.Students.Money;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Students.Money
{
    public class clsFeeTypeData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(string feeName, string description, bool isActive)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_FeeType_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@FeeName", feeName);
            cmd.Parameters.AddWithValue("@Description", (object)description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsActive", isActive);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int feeTypeID, string feeName, string description, bool isActive)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_FeeType_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@FeeTypeID", feeTypeID);
            cmd.Parameters.AddWithValue("@FeeName", feeName);
            cmd.Parameters.AddWithValue("@Description", (object)description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsActive", isActive);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int feeTypeID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_FeeType_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@FeeTypeID", feeTypeID);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static FeeTypeDTO GetByID(int feeTypeID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_FeeType_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@FeeTypeID", feeTypeID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new FeeTypeDTO(
                    (int)reader["FeeTypeID"],
                    reader["FeeName"].ToString(),
                    reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                    (bool)reader["IsActive"]
                );
            }
            return null;
        }

        public static List<FeeTypeDTO> GetAll()
        {
            List<FeeTypeDTO> list = new();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_FeeType_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new FeeTypeDTO(
                    (int)reader["FeeTypeID"],
                    reader["FeeName"].ToString(),
                    reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }

        public static List<FeeTypeDTO> GetActive()
        {
            List<FeeTypeDTO> list = new();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_FeeType_GetActive", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new FeeTypeDTO(
                    (int)reader["FeeTypeID"],
                    reader["FeeName"].ToString(),
                    reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }
    }

}

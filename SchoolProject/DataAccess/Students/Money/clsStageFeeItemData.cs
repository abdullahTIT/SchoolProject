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
    public class clsStageFeeItemData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int stageID, int feeTypeID, int academicYearID, decimal amount, string description, bool isActive)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StageFeeItem_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StageID", stageID);
            cmd.Parameters.AddWithValue("@FeeTypeID", feeTypeID);
            cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@Description", (object)description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsActive", isActive);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int id, int stageID, int feeTypeID, int academicYearID, decimal amount, string description, bool isActive)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StageFeeItem_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StageFeeItemID", id);
            cmd.Parameters.AddWithValue("@StageID", stageID);
            cmd.Parameters.AddWithValue("@FeeTypeID", feeTypeID);
            cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@Description", (object)description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IsActive", isActive);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StageFeeItem_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StageFeeItemID", id);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static StageFeeItemDTO GetByID(int id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StageFeeItem_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StageFeeItemID", id);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new StageFeeItemDTO(
                    (int)reader["StageFeeItemID"],
                    (int)reader["StageID"],
                    (int)reader["FeeTypeID"],
                    (int)reader["AcademicYearID"],
                    (decimal)reader["Amount"],
                    reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                    (bool)reader["IsActive"]
                );
            }
            return null;
        }

        public static List<StageFeeItemDTO> GetAll()
        {
            List<StageFeeItemDTO> list = new();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StageFeeItem_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StageFeeItemDTO(
                    (int)reader["StageFeeItemID"],
                    (int)reader["StageID"],
                    (int)reader["FeeTypeID"],
                    (int)reader["AcademicYearID"],
                    (decimal)reader["Amount"],
                    reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }

        public static List<StageFeeItemDTO> GetActive()
        {
            List<StageFeeItemDTO> list = new();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StageFeeItem_GetActive", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StageFeeItemDTO(
                    (int)reader["StageFeeItemID"],
                    (int)reader["StageID"],
                    (int)reader["FeeTypeID"],
                    (int)reader["AcademicYearID"],
                    (decimal)reader["Amount"],
                    reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }
    }

}
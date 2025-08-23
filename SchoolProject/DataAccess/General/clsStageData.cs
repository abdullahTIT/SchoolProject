using GlobalLayer;
using Microsoft.Data.SqlClient;
using Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.General
{
    public class clsStageData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int InsertStage(string stageName, bool isActive)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Stage_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StageName", stageName);
            cmd.Parameters.AddWithValue("@IsActive", isActive);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool UpdateStage(int stageID, string stageName, bool isActive)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Stage_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StageID", stageID);
            cmd.Parameters.AddWithValue("@StageName", stageName);
            cmd.Parameters.AddWithValue("@IsActive", isActive);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool DeleteStage(int stageID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Stage_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StageID", stageID);
            conn.Open();

            object result = cmd.ExecuteScalar();
            return result != null && Convert.ToInt32(result) > 0;
        }

        public static StageDTO GetStageByID(int stageID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Stage_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StageID", stageID);
            conn.Open();

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new StageDTO(
                    (int)reader["StageID"],
                    reader["StageName"].ToString(),
                    (bool)reader["IsActive"]
                );
            }
            return null;
        }

        public static List<StageDTO> GetAllStages()
        {
            List<StageDTO> list = new();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Stage_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StageDTO(
                    (int)reader["StageID"],
                    reader["StageName"].ToString(),
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }

        public static List<StageDTO> GetActiveStages()
        {
            List<StageDTO> list = new();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Stage_GetActive", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StageDTO(
                    (int)reader["StageID"],
                    reader["StageName"].ToString(),
                    (bool)reader["IsActive"]
                ));
            }
            return list;
        }
    }

}

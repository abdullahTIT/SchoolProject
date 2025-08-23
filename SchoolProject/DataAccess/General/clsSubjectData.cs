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
    public class clsSubjectData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(string subjectName, int stageID, bool isActive, string notes)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Subject_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SubjectName", subjectName);
            cmd.Parameters.AddWithValue("@StageID", stageID);
            cmd.Parameters.AddWithValue("@IsActive", isActive);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int subjectID, string subjectName, int stageID, bool isActive, string notes)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Subject_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SubjectID", subjectID);
            cmd.Parameters.AddWithValue("@SubjectName", subjectName);
            cmd.Parameters.AddWithValue("@StageID", stageID);
            cmd.Parameters.AddWithValue("@IsActive", isActive);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int subjectID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Subject_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SubjectID", subjectID);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static SubjectDTO GetByID(int subjectID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Subject_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SubjectID", subjectID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new SubjectDTO(
                    (int)reader["SubjectID"],
                    reader["SubjectName"].ToString(),
                    (int)reader["StageID"],
                    (bool)reader["IsActive"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                );
            }
            return null;
        }

        public static List<SubjectDTO> GetAll()
        {
            List<SubjectDTO> list = new List<SubjectDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Subject_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new SubjectDTO(
                    (int)reader["SubjectID"],
                    reader["SubjectName"].ToString(),
                    (int)reader["StageID"],
                    (bool)reader["IsActive"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }

        public static List<SubjectDTO> GetActive()
        {
            List<SubjectDTO> list = new List<SubjectDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Subject_GetActive", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new SubjectDTO(
                    (int)reader["SubjectID"],
                    reader["SubjectName"].ToString(),
                    (int)reader["StageID"],
                    (bool)reader["IsActive"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }

        public static List<SubjectDTO> GetByStageID(int stageID)
        {
            List<SubjectDTO> list = new List<SubjectDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Subject_GetByStageID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StageID", stageID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new SubjectDTO(
                    (int)reader["SubjectID"],
                    reader["SubjectName"].ToString(),
                    (int)reader["StageID"],
                    (bool)reader["IsActive"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }
    }


}

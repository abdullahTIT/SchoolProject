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
    public class clsGuardianData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int InsertGuardian(int personID, string jobs, string notes)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Guardian_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PersonID", personID);
                cmd.Parameters.AddWithValue("@Jobs", jobs);
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

        public static bool UpdateGuardian(int guardianID, int personID, string jobs, string notes)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Guardian_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@GuardianID", guardianID);
                cmd.Parameters.AddWithValue("@PersonID", personID);
                cmd.Parameters.AddWithValue("@Jobs", jobs);
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

        public static bool DeleteGuardian(int guardianID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Guardian_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@GuardianID", guardianID);

                conn.Open();
                object result = cmd.ExecuteScalar();
                int rowsAffected = result != null ? Convert.ToInt32(result) : 0;

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }


        public static GuardianDTO GetGuardianByID(int guardianID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Guardian_GetByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@GuardianID", guardianID);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new GuardianDTO(
                        (int)reader["GuardianID"],
                        (int)reader["PersonID"],
                        reader["Jobs"].ToString(),
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

        public static List<GuardianDTO> GetGuardiansByPersonID(int personID)
        {
            List<GuardianDTO> list = new List<GuardianDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Guardian_GetByPersonID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PersonID", personID);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new GuardianDTO(
                        (int)reader["GuardianID"],
                        (int)reader["PersonID"],
                        reader["Jobs"].ToString(),
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

        public static List<GuardianDTO> GetAllGuardians()
        {
            List<GuardianDTO> list = new List<GuardianDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Guardian_GetAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new GuardianDTO(
                        (int)reader["GuardianID"],
                        (int)reader["PersonID"],
                        reader["Jobs"].ToString(),
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

        public static bool DoesPersonHaveGuardian(int personID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Guardian_ExistsByPersonID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PersonID", personID);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result) == 1;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static bool IsPersonLinkedToGuardian(int personID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Guardian_ExistsByPersonID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PersonID", personID);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int exists))
                {
                    return exists == 1;
                }
                return false;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }


    }

}


using GlobalLayer;
using Microsoft.Data.SqlClient;
using Models.Persons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Persons
{
    public class IdentifierData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int InsertIdentifier(int identifierTypeID, string identifierValue)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("Insert_Identifier", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdentifierTypeID", identifierTypeID);
                    cmd.Parameters.AddWithValue("@IdentifierValue", identifierValue ?? (object)DBNull.Value);

                    var outputId = new SqlParameter("@NewID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputId);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    return (int)outputId.Value;
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);

                return -1;
            }
        }

        public static bool UpdateIdentifier(int identifierID, int identifierTypeID, string identifierValue)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("Update_Identifier", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdentifierID", identifierID);
                    cmd.Parameters.AddWithValue("@IdentifierTypeID", identifierTypeID);
                    cmd.Parameters.AddWithValue("@IdentifierValue", identifierValue ?? (object)DBNull.Value);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static bool DeleteIdentifier(int identifierID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("Delete_Identifier", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdentifierID", identifierID);

                    conn.Open();
                    int rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }


        public static IdentifierDTO GetIdentifierByID(int identifierID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("Get_IdentifierByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdentifierID", identifierID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new IdentifierDTO(
                                (int)reader["IdentifierID"],
                                (int)reader["IdentifierTypeID"],
                                reader["IdentifierValue"]?.ToString()
                            );
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return null;
            }
        }

        public static List<IdentifierDTO> GetAllIdentifiers()
        {
            List<IdentifierDTO> list = new List<IdentifierDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("Get_All_Identifiers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new IdentifierDTO(
                                (int)reader["IdentifierID"],
                                (int)reader["IdentifierTypeID"],
                                reader["IdentifierValue"]?.ToString()
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        public static bool DoesIdentifierExist(string identifierValue, int? excludeID = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.DoesIdentifierExist", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdentifierValue", identifierValue ?? (object)DBNull.Value);

                    if (excludeID.HasValue)
                        cmd.Parameters.AddWithValue("@ExcludeID", excludeID.Value);
                    else
                        cmd.Parameters.AddWithValue("@ExcludeID", DBNull.Value);

                    conn.Open();

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }
    }

}

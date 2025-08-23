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
    public class IdentifierTypeData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static List<IdentifierTypeDTO> GetAllIdentifierTypes()
        {
            var list = new List<IdentifierTypeDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("Get_All_IdentifierTypes", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new IdentifierTypeDTO(
                                reader.GetInt32(reader.GetOrdinal("IdentifierTypeID")),
                                reader.GetString(reader.GetOrdinal("IdentifierName"))
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

        public static IdentifierTypeDTO GetIdentifierTypeByID(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("Get_IdentifierType_ByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdentifierTypeID", id);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new IdentifierTypeDTO(
                                reader.GetInt32(reader.GetOrdinal("IdentifierTypeID")),
                                reader.GetString(reader.GetOrdinal("IdentifierName"))
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return null;
        }

        public static int InsertIdentifierType(string identifierName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("Insert_IdentifierType", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdentifierName", identifierName);

                    var outputId = new SqlParameter("@NewID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputId); // <<<<< هذا السطر مهم جداً

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

        public static bool UpdateIdentifierType(int identifierTypeID, string identifierName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("Update_IdentifierType", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdentifierTypeID", identifierTypeID);
                    cmd.Parameters.AddWithValue("@IdentifierName", identifierName);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static bool DeleteIdentifierType(int identifierTypeID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("Delete_IdentifierType", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdentifierTypeID", identifierTypeID);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // لم يحصل استثناء = حذف ناجح
                    return true;
                }
            }
            catch (SqlException sqlEx)
            {
                clsErrorLogger.LogError(sqlEx);
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

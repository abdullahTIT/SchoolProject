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

    public class clsPersonData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static bool DoesPhoneExist(string phone, int? excludePersonID = null)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);

            string sql = "SELECT COUNT(*) FROM Persons WHERE Phone = @Phone";

            if (excludePersonID.HasValue)
            {
                sql += " AND PersonID != @ExcludePersonID";
            }

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = phone;

            if (excludePersonID.HasValue)
            {
                cmd.Parameters.Add("@ExcludePersonID", SqlDbType.Int).Value = excludePersonID.Value;
            }

            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        public static bool DoesEmailExist(string email, int? excludePersonID = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false; // أو true حسب ما يناسب منطقك

            using SqlConnection conn = new SqlConnection(_connectionString);
            string sql = "SELECT COUNT(*) FROM Persons WHERE Email = @Email";
            if (excludePersonID.HasValue)
            {
                sql += " AND PersonID != @ExcludePersonID";
            }
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = email;
            if (excludePersonID.HasValue)
            {
                cmd.Parameters.Add("@ExcludePersonID", SqlDbType.Int).Value = excludePersonID.Value;
            }
            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        public static int InsertPerson(string firstName, string secondName, string thirdName, string lastName, int? identifierID,
                               bool gender, DateTime? birthDate, string phone, string email, string address, string imagePath)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Person_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@SecondName", secondName);
                cmd.Parameters.AddWithValue("@ThirdName", thirdName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@IdentifierID", (object)identifierID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@BirthDate", (object)birthDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", (object)address ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ImagePath", (object)imagePath ?? DBNull.Value);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return -1;
            }
        }

        public static bool UpdatePerson(int personID, string firstName, string secondName, string thirdName, string lastName, int? identifierID,
                                bool gender, DateTime? birthDate, string phone, string email, string address, string imagePath)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Person_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PersonID", personID);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@SecondName", secondName);
                cmd.Parameters.AddWithValue("@ThirdName", thirdName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@IdentifierID", (object)identifierID ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@BirthDate", (object)birthDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", (object)address ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ImagePath", (object)imagePath ?? DBNull.Value);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static bool DeletePerson(int personID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Person_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PersonID", personID);

                conn.Open();

                // ExecuteScalar لاستقبال قيمة واحدة من الإجراء
                object result = cmd.ExecuteScalar();
                int rowsDeleted = 0;

                if (result != null && int.TryParse(result.ToString(), out rowsDeleted))
                {
                    return rowsDeleted > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException sqlEx)
            {
                // هنا يمكن قراءة رسائل الخطأ المفصلة خاصة القيود المرجعية
                Console.WriteLine("SQL Error in DeletePerson: " + sqlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static PersonDTO GetPersonByID(int personID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Person_GetByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PersonID", personID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new PersonDTO(
                        (int)reader["PersonID"],
                        reader["FirstName"].ToString(),
                        reader["SecondName"].ToString(),
                        reader["ThirdName"].ToString(),
                        reader["LastName"].ToString(),
                        reader["IdentifierID"] == DBNull.Value ? null : (int)reader["IdentifierID"],
                        (bool)reader["Gender"],
                        reader["BirthDate"] == DBNull.Value ? null : (DateTime)reader["BirthDate"],
                        reader["Phone"].ToString(),
                        reader["Email"] == DBNull.Value ? null : reader["Email"].ToString(),
                        reader["Address"] == DBNull.Value ? null : reader["Address"].ToString(),
                        reader["ImagePath"] == DBNull.Value ? null : reader["ImagePath"].ToString()
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


        public static List<PersonDTO> GetAllPersons()
        {
            List<PersonDTO> list = new List<PersonDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Person_GetAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new PersonDTO(
                        (int)reader["PersonID"],
                        reader["FirstName"].ToString(),
                        reader["SecondName"].ToString(),
                        reader["ThirdName"].ToString(),
                        reader["LastName"].ToString(),
                        reader["IdentifierID"] == DBNull.Value ? null : (int)reader["IdentifierID"],
                        (bool)reader["Gender"],
                        reader["BirthDate"] == DBNull.Value ? null : (DateTime)reader["BirthDate"],
                        reader["Phone"].ToString(),
                        reader["Email"] == DBNull.Value ? null : reader["Email"].ToString(),
                        reader["Address"] == DBNull.Value ? null : reader["Address"].ToString(),
                        reader["ImagePath"] == DBNull.Value ? null : reader["ImagePath"].ToString()  // تم التعديل هنا

                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        public static List<PersonDTO> GetPersonsPaged(int offset = 0, int fetch = 50, string search = null)
        {
            List<PersonDTO> list = new List<PersonDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Person_GetAllPaged", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Offset", offset);
                cmd.Parameters.AddWithValue("@Fetch", fetch);
                if (string.IsNullOrEmpty(search))
                    cmd.Parameters.AddWithValue("@Search", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Search", search);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new PersonDTO(
                        (int)reader["PersonID"],
                        reader["FirstName"].ToString(),
                        reader["SecondName"].ToString(),
                        reader["ThirdName"].ToString(),
                        reader["LastName"].ToString(),
                        reader["IdentifierID"] == DBNull.Value ? null : (int)reader["IdentifierID"],
                        (bool)reader["Gender"],
                        reader["BirthDate"] == DBNull.Value ? null : (DateTime)reader["BirthDate"],
                        reader["Phone"].ToString(),
                        reader["Email"] == DBNull.Value ? null : reader["Email"].ToString(),
                        reader["Address"] == DBNull.Value ? null : reader["Address"].ToString(),
                        reader["ImagePath"] == DBNull.Value ? null : reader["ImagePath"].ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        public static bool IsPersonRelated(int personID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_CheckPersonRelations", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PersonID", personID);
                SqlParameter outputParam = new SqlParameter("@IsRelated", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return Convert.ToBoolean(outputParam.Value);
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return true; // لتجنب الحذف إذا حدث خطأ
            }
        }
        public static PersonDTO GetPersonByPhone(string phoneNumber)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Person_GetByPhone", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new PersonDTO(
                        (int)reader["PersonID"],
                        reader["FirstName"].ToString(),
                        reader["SecondName"].ToString(),
                        reader["ThirdName"].ToString(),
                        reader["LastName"].ToString(),
                        reader["IdentifierID"] == DBNull.Value ? null : (int)reader["IdentifierID"],
                        (bool)reader["Gender"],
                        reader["BirthDate"] == DBNull.Value ? null : (DateTime)reader["BirthDate"],
                        reader["Phone"].ToString(),
                        reader["Email"] == DBNull.Value ? null : reader["Email"].ToString(),
                        reader["Address"] == DBNull.Value ? null : reader["Address"].ToString(),
                        reader["ImagePath"] == DBNull.Value ? null : reader["ImagePath"].ToString()  // تم التعديل هنا

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
    }
}

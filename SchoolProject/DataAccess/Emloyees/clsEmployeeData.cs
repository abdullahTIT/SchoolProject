using GlobalLayer;
using Microsoft.Data.SqlClient;
using Models.Employees;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Emloyees
{
    public class clsEmployeeData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int InsertEmployee(int personID, int jobTitleID, DateTime hireDate,
                                         DateTime? terminationDate, bool isActive, string notes, decimal salary)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Employee_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PersonID", personID);
                cmd.Parameters.AddWithValue("@JobTitleID", jobTitleID);
                cmd.Parameters.AddWithValue("@HireDate", hireDate);
                cmd.Parameters.AddWithValue("@TerminationDate", (object)terminationDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Salary", salary);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return -1;
            }
        }

        public static bool UpdateEmployee(int employeeID, int personID, int jobTitleID, DateTime hireDate,
                                          DateTime? terminationDate, bool isActive, string notes, decimal salary)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Employee_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@PersonID", personID);
                cmd.Parameters.AddWithValue("@JobTitleID", jobTitleID);
                cmd.Parameters.AddWithValue("@HireDate", hireDate);
                cmd.Parameters.AddWithValue("@TerminationDate", (object)terminationDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Salary", salary);

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

        public static int DeleteEmployee(int employeeID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Employee_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int rowsAffected))
                    return rowsAffected;
                return 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return 0;
            }
        }

        public static EmployeeDTO GetEmployeeByID(int employeeID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Employee_GetByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new EmployeeDTO(
                        (int)reader["EmployeeID"],
                        (int)reader["PersonID"],
                        (int)reader["JobTitleID"],
                        (DateTime)reader["HireDate"],
                        reader["TerminationDate"] == DBNull.Value ? null : (DateTime)reader["TerminationDate"],
                        (bool)reader["IsActive"],
                        reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                        (decimal)reader["Salary"]
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

        public static EmployeeDTO GetEmployeeByPhone(string phone)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Employee_GetByPhone", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Phone", phone);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new EmployeeDTO(
                        (int)reader["EmployeeID"],
                        (int)reader["PersonID"],
                        (int)reader["JobTitleID"],
                        (DateTime)reader["HireDate"],
                        reader["TerminationDate"] == DBNull.Value ? null : (DateTime?)reader["TerminationDate"],
                        (bool)reader["IsActive"],
                        reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                        (decimal)reader["Salary"]
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
        public static EmployeeDTO GetEmployeeByPersonID(int personID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Employee_GetByPersonID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PersonID", personID);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new EmployeeDTO(
                        (int)reader["EmployeeID"],
                        (int)reader["PersonID"],
                        (int)reader["JobTitleID"],
                        (DateTime)reader["HireDate"],
                        reader["TerminationDate"] == DBNull.Value ? null : (DateTime?)reader["TerminationDate"],
                        (bool)reader["IsActive"],
                        reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                        (decimal)reader["Salary"]
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

        public static List<EmployeeDTO> GetAllEmployees()
        {
            List<EmployeeDTO> list = new List<EmployeeDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Employee_GetAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new EmployeeDTO(
                        (int)reader["EmployeeID"],
                        (int)reader["PersonID"],
                        (int)reader["JobTitleID"],
                        (DateTime)reader["HireDate"],
                        reader["TerminationDate"] == DBNull.Value ? null : (DateTime)reader["TerminationDate"],
                        (bool)reader["IsActive"],
                        reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                        (decimal)reader["Salary"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        public static List<EmployeeDTO> GetActiveEmployees()
        {
            List<EmployeeDTO> list = new List<EmployeeDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_Employee_GetActive", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new EmployeeDTO(
                        (int)reader["EmployeeID"],
                        (int)reader["PersonID"],
                        (int)reader["JobTitleID"],
                        (DateTime)reader["HireDate"],
                        reader["TerminationDate"] == DBNull.Value ? null : (DateTime)reader["TerminationDate"],
                        (bool)reader["IsActive"],
                        reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                        (decimal)reader["Salary"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        public static bool IsEmployeeExistByPersonID(int personID)
        {
            bool exists = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("IsEmployeeExistByPersonID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", personID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                exists = reader.GetBoolean(reader.GetOrdinal("ExistsFlag"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return exists;
        }

        public static bool IsEmployeeExistByPersonIDAndNotThisEmployee(int personID, int employeeID)
        {
            bool exists = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("IsEmployeeExistByPersonIDAndNotThisEmployee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", personID);
                        command.Parameters.AddWithValue("@EmployeeID", employeeID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                exists = reader.GetBoolean(reader.GetOrdinal("ExistsFlag"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return exists;
        }

    }

}

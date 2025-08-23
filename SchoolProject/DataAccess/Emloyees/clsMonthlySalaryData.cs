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


    public class clsMonthlySalaryData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int employeeID, DateTime salaryMonth, decimal baseSalary,
            int absenceDays, decimal deductionPerAbsent, bool paid, DateTime? paymentDate)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@SalaryMonth", salaryMonth);
                cmd.Parameters.AddWithValue("@BaseSalary", baseSalary);
                cmd.Parameters.AddWithValue("@AbsenceDays", absenceDays);
                cmd.Parameters.AddWithValue("@DeductionPerAbsent", deductionPerAbsent);
                cmd.Parameters.AddWithValue("@Paid", paid);
                cmd.Parameters.AddWithValue("@PaymentDate", (object)paymentDate ?? DBNull.Value);

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

        public static bool Update(int salaryID, int employeeID, DateTime salaryMonth, decimal baseSalary,
            int absenceDays, decimal deductionPerAbsent, bool paid, DateTime? paymentDate)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@SalaryID", salaryID);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@SalaryMonth", salaryMonth);
                cmd.Parameters.AddWithValue("@BaseSalary", baseSalary);
                cmd.Parameters.AddWithValue("@AbsenceDays", absenceDays);
                cmd.Parameters.AddWithValue("@DeductionPerAbsent", deductionPerAbsent);
                cmd.Parameters.AddWithValue("@Paid", paid);
                cmd.Parameters.AddWithValue("@PaymentDate", (object)paymentDate ?? DBNull.Value);

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

        public static bool Delete(int salaryID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@SalaryID", salaryID);

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

        public static MonthlySalaryDTO GetByID(int salaryID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_GetByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@SalaryID", salaryID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new MonthlySalaryDTO(
                        (int)reader["SalaryID"],
                        (int)reader["EmployeeID"],
                        (DateTime)reader["SalaryMonth"],
                        (decimal)reader["BaseSalary"],
                        (int)reader["AbsenceDays"],
                        (decimal)reader["DeductionPerAbsent"],
                        (bool)reader["Paid"],
                        reader["PaymentDate"] == DBNull.Value ? null : (DateTime)reader["PaymentDate"]
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

        public static List<MonthlySalaryDTO> GetAll()
        {
            List<MonthlySalaryDTO> list = new List<MonthlySalaryDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_GetAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new MonthlySalaryDTO(
                        (int)reader["SalaryID"],
                        (int)reader["EmployeeID"],
                        (DateTime)reader["SalaryMonth"],
                        (decimal)reader["BaseSalary"],
                        (int)reader["AbsenceDays"],
                        (decimal)reader["DeductionPerAbsent"],
                        (bool)reader["Paid"],
                        reader["PaymentDate"] == DBNull.Value ? null : (DateTime)reader["PaymentDate"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        public static MonthlySalaryDTO GetByEmployeeAndMonth(int employeeID, DateTime salaryMonth)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_GetByEmployeeAndMonth", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@SalaryMonth", salaryMonth);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new MonthlySalaryDTO(
                        (int)reader["SalaryID"],
                        (int)reader["EmployeeID"],
                        (DateTime)reader["SalaryMonth"],
                        (decimal)reader["BaseSalary"],
                        (int)reader["AbsenceDays"],
                        (decimal)reader["DeductionPerAbsent"],
                        (bool)reader["Paid"],
                        reader["PaymentDate"] == DBNull.Value ? null : (DateTime)reader["PaymentDate"]
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

        public static (int TotalSalaries, decimal TotalNetSalary, decimal TotalPaid, decimal TotalUnpaid) GetSumsByMonth(DateTime salaryMonth)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_GetSumsByMonth", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@SalaryMonth", salaryMonth);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int totalSalaries = (int)reader["TotalSalaries"];
                    decimal totalNetSalary = reader["TotalNetSalary"] == DBNull.Value ? 0 : (decimal)reader["TotalNetSalary"];
                    decimal totalPaid = reader["TotalPaid"] == DBNull.Value ? 0 : (decimal)reader["TotalPaid"];
                    decimal totalUnpaid = reader["TotalUnpaid"] == DBNull.Value ? 0 : (decimal)reader["TotalUnpaid"];

                    return (totalSalaries, totalNetSalary, totalPaid, totalUnpaid);
                }
                return (0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return (0, 0, 0, 0);
            }
        }

        public static List<MonthlySalaryDTO> GetAllByEmployeeID(int employeeID)
        {
            List<MonthlySalaryDTO> list = new List<MonthlySalaryDTO>();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_GetAllByEmployeeID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new MonthlySalaryDTO(
                        (int)reader["SalaryID"],
                        (int)reader["EmployeeID"],
                        (DateTime)reader["SalaryMonth"],
                        (decimal)reader["BaseSalary"],
                        (int)reader["AbsenceDays"],
                        (decimal)reader["DeductionPerAbsent"],
                        (bool)reader["Paid"],
                        reader["PaymentDate"] == DBNull.Value ? null : (DateTime)reader["PaymentDate"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        public static bool UpdatePaidStatus(int salaryID, bool paid, DateTime? paymentDate)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_UpdatePaidStatus", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@SalaryID", salaryID);
                cmd.Parameters.AddWithValue("@Paid", paid);
                cmd.Parameters.AddWithValue("@PaymentDate", (object)paymentDate ?? DBNull.Value);

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

        public static MonthlySalaryDTO GetLastByEmployee(int employeeID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_GetLastByEmployee", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new MonthlySalaryDTO(
                        (int)reader["SalaryID"],
                        (int)reader["EmployeeID"],
                        (DateTime)reader["SalaryMonth"],
                        (decimal)reader["BaseSalary"],
                        (int)reader["AbsenceDays"],
                        (decimal)reader["DeductionPerAbsent"],
                        (bool)reader["Paid"],
                        reader["PaymentDate"] == DBNull.Value ? null : (DateTime)reader["PaymentDate"]
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

        public static (int SalaryCount, decimal TotalNetSalary, decimal TotalPaid, decimal TotalUnpaid)
            GetSummaryByEmployeeAndPeriod(int employeeID, DateTime startDate, DateTime endDate)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_GetSummaryByEmployeeAndPeriod", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return (
                        (int)reader["SalaryCount"],
                        reader["TotalNetSalary"] == DBNull.Value ? 0 : (decimal)reader["TotalNetSalary"],
                        reader["TotalPaid"] == DBNull.Value ? 0 : (decimal)reader["TotalPaid"],
                        reader["TotalUnpaid"] == DBNull.Value ? 0 : (decimal)reader["TotalUnpaid"]
                    );
                }
                return (0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return (0, 0, 0, 0);
            }
        }

        public static List<MonthlySalaryDTO> GetUnpaidByMonth(DateTime salaryMonth)
        {
            List<MonthlySalaryDTO> list = new();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_GetUnpaidByMonth", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@SalaryMonth", salaryMonth);

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new MonthlySalaryDTO(
                        (int)reader["SalaryID"],
                        (int)reader["EmployeeID"],
                        (DateTime)reader["SalaryMonth"],
                        (decimal)reader["BaseSalary"],
                        (int)reader["AbsenceDays"],
                        (decimal)reader["DeductionPerAbsent"],
                        (bool)reader["Paid"],
                        reader["PaymentDate"] == DBNull.Value ? null : (DateTime)reader["PaymentDate"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        public static bool PartialUpdate(int salaryID, decimal? baseSalary = null,
            int? absenceDays = null, decimal? deductionPerAbsent = null,
            bool? paid = null, DateTime? paymentDate = null)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_PartialUpdate", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@SalaryID", salaryID);
                cmd.Parameters.AddWithValue("@BaseSalary", (object?)baseSalary ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AbsenceDays", (object?)absenceDays ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DeductionPerAbsent", (object?)deductionPerAbsent ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Paid", (object?)paid ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PaymentDate", (object?)paymentDate ?? DBNull.Value);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static decimal GetAverageNetSalaryByPeriod(DateTime startDate, DateTime endDate)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_GetAverageNetSalaryByPeriod", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return result == DBNull.Value ? 0 : Convert.ToDecimal(result);
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return 0;
            }
        }

        public static List<(int Year, int Month)> GetMonthsWithUnpaid()
        {
            List<(int, int)> result = new();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_GetMonthsWithUnpaid", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(((int)reader["Year"], (int)reader["Month"]));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return result;
        }

        public static decimal GetTotalDeductionByEmployeePeriod(int employeeID, DateTime startDate, DateTime endDate)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_MonthlySalary_GetTotalDeductionByEmployeePeriod", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                conn.Open();
                object result = cmd.ExecuteScalar();
                return result == DBNull.Value ? 0 : Convert.ToDecimal(result);
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return 0;
            }

        }


    }

}

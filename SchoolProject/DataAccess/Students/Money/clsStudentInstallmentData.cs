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
    public class clsStudentInstallmentData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int studentFeeID, DateTime dueDate, decimal amount, bool? isPaid, string notes)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentInstallment_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StudentFeeID", studentFeeID);
                cmd.Parameters.AddWithValue("@DueDate", dueDate);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@IsPaid", (object)isPaid ?? DBNull.Value);
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

        public static bool Update(int installmentID, int studentFeeID, DateTime dueDate, decimal amount, bool? isPaid, string notes)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentInstallment_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@InstallmentID", installmentID);
                cmd.Parameters.AddWithValue("@StudentFeeID", studentFeeID);
                cmd.Parameters.AddWithValue("@DueDate", dueDate);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@IsPaid", (object)isPaid ?? DBNull.Value);
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

        public static bool Delete(int installmentID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentInstallment_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@InstallmentID", installmentID);
                conn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static StudentInstallmentDTO GetByID(int installmentID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentInstallment_GetByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@InstallmentID", installmentID);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new StudentInstallmentDTO(
                        (int)reader["InstallmentID"],
                        (int)reader["StudentFeeID"],
                        (DateTime)reader["DueDate"],
                        (decimal)reader["Amount"],
                        reader["IsPaid"] != DBNull.Value ? (bool?)reader["IsPaid"] : null,
                        reader["Notes"]?.ToString()
                    );
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return null;
        }

        public static List<StudentInstallmentDTO> GetAll()
        {
            List<StudentInstallmentDTO> list = new();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentInstallment_GetAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StudentInstallmentDTO(
                        (int)reader["InstallmentID"],
                        (int)reader["StudentFeeID"],
                        (DateTime)reader["DueDate"],
                        (decimal)reader["Amount"],
                        reader["IsPaid"] != DBNull.Value ? (bool?)reader["IsPaid"] : null,
                        reader["Notes"]?.ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }

        // دالة جلب الأقساط غير المدفوعة فقط
        public static List<StudentInstallmentDTO> GetUnpaid()
        {
            List<StudentInstallmentDTO> list = new();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentInstallment_GetUnpaid", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StudentInstallmentDTO(
                        (int)reader["InstallmentID"],
                        (int)reader["StudentFeeID"],
                        (DateTime)reader["DueDate"],
                        (decimal)reader["Amount"],
                        reader["IsPaid"] != DBNull.Value ? (bool?)reader["IsPaid"] : null,
                        reader["Notes"]?.ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        // دالة جلب الأقساط حسب معرف الطالب (يتطلب الربط مع جدول StudentFee)
        public static List<StudentInstallmentDTO> GetByStudentID(int studentID)
        {
            List<StudentInstallmentDTO> list = new();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentInstallment_GetByStudentID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StudentInstallmentDTO(
                        (int)reader["InstallmentID"],
                        (int)reader["StudentFeeID"],
                        (DateTime)reader["DueDate"],
                        (decimal)reader["Amount"],
                        reader["IsPaid"] != DBNull.Value ? (bool?)reader["IsPaid"] : null,
                        reader["Notes"]?.ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        // دالة جلب الأقساط حسب فترة زمنية (حسب DueDate)
        public static List<StudentInstallmentDTO> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            List<StudentInstallmentDTO> list = new();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentInstallment_GetByDateRange", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StudentInstallmentDTO(
                        (int)reader["InstallmentID"],
                        (int)reader["StudentFeeID"],
                        (DateTime)reader["DueDate"],
                        (decimal)reader["Amount"],
                        reader["IsPaid"] != DBNull.Value ? (bool?)reader["IsPaid"] : null,
                        reader["Notes"]?.ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }
            return list;
        }

        // دالة حساب المبلغ الإجمالي المتبقي (مجموع Amount للأقساط غير المدفوعة)
        public static decimal GetTotalRemainingAmount()
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentInstallment_GetTotalRemaining", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return 0;
            }
        }
    }

}

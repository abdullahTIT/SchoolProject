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
    public class clsDeductionData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int employeeID, decimal amount, string reason, DateTime date, DateTime appliedInSalaryMonth)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Deduction_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@Reason", (object)reason ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Parameters.AddWithValue("@AppliedInSalaryMonth", appliedInSalaryMonth);

            conn.Open();
            var result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }

        public static bool Update(int deductionID, int employeeID, decimal amount, string reason, DateTime date, DateTime appliedInSalaryMonth)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Deduction_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@DeductionID", deductionID);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@Reason", (object)reason ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Parameters.AddWithValue("@AppliedInSalaryMonth", appliedInSalaryMonth);

            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public static bool Delete(int deductionID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Deduction_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@DeductionID", deductionID);

            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public static DeductionDTO GetByID(int deductionID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Deduction_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@DeductionID", deductionID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new DeductionDTO(
                    (int)reader["DeductionID"],
                    (int)reader["EmployeeID"],
                    (decimal)reader["Amount"],
                    reader["Reason"]?.ToString(),
                    (DateTime)reader["Date"],
                    (DateTime)reader["AppliedInSalaryMonth"]
                );
            }
            return null;
        }

        public static List<DeductionDTO> GetAll()
        {
            List<DeductionDTO> list = new List<DeductionDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Deduction_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new DeductionDTO(
                    (int)reader["DeductionID"],
                    (int)reader["EmployeeID"],
                    (decimal)reader["Amount"],
                    reader["Reason"]?.ToString(),
                    (DateTime)reader["Date"],
                    (DateTime)reader["AppliedInSalaryMonth"]
                ));
            }
            return list;
        }

        public static List<DeductionDTO> GetByEmployeeID(int employeeID)
        {
            List<DeductionDTO> list = new List<DeductionDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Deduction_GetByEmployeeID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new DeductionDTO(
                    (int)reader["DeductionID"],
                    (int)reader["EmployeeID"],
                    (decimal)reader["Amount"],
                    reader["Reason"]?.ToString(),
                    (DateTime)reader["Date"],
                    (DateTime)reader["AppliedInSalaryMonth"]
                ));
            }
            return list;
        }
        // حساب مجموع الخصومات لموظف معين بين تاريخين (حسب تاريخ الخصم)
        public static decimal GetTotalDeductionsByEmployeeInRange(int employeeID, DateTime fromDate, DateTime toDate)
        {
            decimal total = 0m;
            using SqlConnection conn = new SqlConnection(_connectionString);
            string sql = @"
        SELECT ISNULL(SUM(Amount), 0) 
        FROM Deductions
        WHERE EmployeeID = @EmployeeID 
          AND Date BETWEEN @FromDate AND @ToDate";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@FromDate", fromDate);
            cmd.Parameters.AddWithValue("@ToDate", toDate);

            conn.Open();
            object result = cmd.ExecuteScalar();
            if (result != null && decimal.TryParse(result.ToString(), out decimal sum))
                total = sum;

            return total;
        }

        // جلب الخصومات التي تم تطبيقها في شهر معين (AppliedInSalaryMonth)
        public static List<DeductionDTO> GetDeductionsBySalaryMonth(DateTime salaryMonth)
        {
            List<DeductionDTO> list = new List<DeductionDTO>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            string sql = @"
        SELECT * FROM Deductions 
        WHERE AppliedInSalaryMonth = @SalaryMonth
        ORDER BY Date DESC";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@SalaryMonth", salaryMonth);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new DeductionDTO(
                    (int)reader["DeductionID"],
                    (int)reader["EmployeeID"],
                    (decimal)reader["Amount"],
                    reader["Reason"]?.ToString(),
                    (DateTime)reader["Date"],
                    (DateTime)reader["AppliedInSalaryMonth"]
                ));
            }
            return list;
        }

        // دالة بحث مرنة باستخدام شروط اختيارية (بحث ديناميكي)
        public static List<DeductionDTO> SearchDeductions(int? employeeID = null, DateTime? fromDate = null, DateTime? toDate = null, decimal? minAmount = null, decimal? maxAmount = null)
        {
            List<DeductionDTO> list = new List<DeductionDTO>();
            using SqlConnection conn = new SqlConnection(_connectionString);

            var sb = new StringBuilder();
            sb.Append("SELECT * FROM Deductions WHERE 1=1 ");

            if (employeeID.HasValue)
                sb.Append("AND EmployeeID = @EmployeeID ");
            if (fromDate.HasValue)
                sb.Append("AND Date >= @FromDate ");
            if (toDate.HasValue)
                sb.Append("AND Date <= @ToDate ");
            if (minAmount.HasValue)
                sb.Append("AND Amount >= @MinAmount ");
            if (maxAmount.HasValue)
                sb.Append("AND Amount <= @MaxAmount ");

            sb.Append("ORDER BY Date DESC");

            using SqlCommand cmd = new SqlCommand(sb.ToString(), conn);

            if (employeeID.HasValue)
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID.Value);
            if (fromDate.HasValue)
                cmd.Parameters.AddWithValue("@FromDate", fromDate.Value);
            if (toDate.HasValue)
                cmd.Parameters.AddWithValue("@ToDate", toDate.Value);
            if (minAmount.HasValue)
                cmd.Parameters.AddWithValue("@MinAmount", minAmount.Value);
            if (maxAmount.HasValue)
                cmd.Parameters.AddWithValue("@MaxAmount", maxAmount.Value);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new DeductionDTO(
                    (int)reader["DeductionID"],
                    (int)reader["EmployeeID"],
                    (decimal)reader["Amount"],
                    reader["Reason"]?.ToString(),
                    (DateTime)reader["Date"],
                    (DateTime)reader["AppliedInSalaryMonth"]
                ));
            }
            return list;
        }

    }

}

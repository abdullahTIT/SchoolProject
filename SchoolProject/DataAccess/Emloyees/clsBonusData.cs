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
    public class clsBonusData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int employeeID, decimal amount, string reason, DateTime date, DateTime appliedInSalaryMonth)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Bonus_Insert", conn)
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

        public static bool Update(int bonusID, int employeeID, decimal amount, string reason, DateTime date, DateTime appliedInSalaryMonth)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Bonus_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@BonusID", bonusID);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@Reason", (object)reason ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Parameters.AddWithValue("@AppliedInSalaryMonth", appliedInSalaryMonth);

            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public static bool Delete(int bonusID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Bonus_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@BonusID", bonusID);

            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public static BonusDTO GetByID(int bonusID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Bonus_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@BonusID", bonusID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new BonusDTO(
                    (int)reader["BonusID"],
                    (int)reader["EmployeeID"],
                    (decimal)reader["Amount"],
                    reader["Reason"]?.ToString(),
                    (DateTime)reader["Date"],
                    (DateTime)reader["AppliedInSalaryMonth"]
                );
            }
            return null;
        }

        public static List<BonusDTO> GetAll()
        {
            List<BonusDTO> list = new List<BonusDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Bonus_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new BonusDTO(
                    (int)reader["BonusID"],
                    (int)reader["EmployeeID"],
                    (decimal)reader["Amount"],
                    reader["Reason"]?.ToString(),
                    (DateTime)reader["Date"],
                    (DateTime)reader["AppliedInSalaryMonth"]
                ));
            }
            return list;
        }

        public static List<BonusDTO> GetByEmployeeID(int employeeID)
        {
            List<BonusDTO> list = new List<BonusDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Bonus_GetByEmployeeID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new BonusDTO(
                    (int)reader["BonusID"],
                    (int)reader["EmployeeID"],
                    (decimal)reader["Amount"],
                    reader["Reason"]?.ToString(),
                    (DateTime)reader["Date"],
                    (DateTime)reader["AppliedInSalaryMonth"]
                ));
            }
            return list;
        }
        // دالة لحساب مجموع المكافآت لموظف خلال فترة زمنية
        public static decimal GetTotalAmountByEmployeeAndPeriod(int employeeID, DateTime startDate, DateTime endDate)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Bonus_GetTotalAmountByEmployeeAndPeriod", conn)
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

        // دالة لجلب المكافآت حسب AppliedInSalaryMonth
        public static List<BonusDTO> GetByAppliedSalaryMonth(int employeeID, DateTime appliedInSalaryMonth)
        {
            List<BonusDTO> list = new List<BonusDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Bonus_GetByAppliedSalaryMonth", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@AppliedInSalaryMonth", appliedInSalaryMonth);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new BonusDTO(
                    (int)reader["BonusID"],
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

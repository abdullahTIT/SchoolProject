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
    public class clsEmployeeSalaryData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int employeeID, DateTime effectiveDate, decimal baseSalary, decimal allowances, decimal deductions, string notes)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_EmployeeSalaryHistory_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@EffectiveDate", effectiveDate);
            cmd.Parameters.AddWithValue("@BaseSalary", baseSalary);
            cmd.Parameters.AddWithValue("@Allowances", allowances);
            cmd.Parameters.AddWithValue("@Deductions", deductions);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int salaryID, int employeeID, DateTime effectiveDate, decimal baseSalary, decimal allowances, decimal deductions, string notes)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_EmployeeSalaryHistory_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SalaryID", salaryID);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@EffectiveDate", effectiveDate);
            cmd.Parameters.AddWithValue("@BaseSalary", baseSalary);
            cmd.Parameters.AddWithValue("@Allowances", allowances);
            cmd.Parameters.AddWithValue("@Deductions", deductions);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int salaryID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_EmployeeSalaryHistory_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SalaryID", salaryID);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static EmployeeSalaryDTO GetByID(int salaryID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_EmployeeSalaryHistory_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@SalaryID", salaryID);
            conn.Open();

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new EmployeeSalaryDTO(
                    (int)reader["SalaryID"],
                    (int)reader["EmployeeID"],
                    (DateTime)reader["EffectiveDate"],
                    (decimal)reader["BaseSalary"],
                    (decimal)reader["Allowances"],
                    (decimal)reader["Deductions"],
                    reader["Notes"]?.ToString()
                );
            }
            return null;
        }

        public static List<EmployeeSalaryDTO> GetAll()
        {
            List<EmployeeSalaryDTO> list = new();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_EmployeeSalaryHistory_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new EmployeeSalaryDTO(
                    (int)reader["SalaryID"],
                    (int)reader["EmployeeID"],
                    (DateTime)reader["EffectiveDate"],
                    (decimal)reader["BaseSalary"],
                    (decimal)reader["Allowances"],
                    (decimal)reader["Deductions"],
                    reader["Notes"]?.ToString()
                ));
            }
            return list;
        }

        public static List<EmployeeSalaryDTO> GetByEmployeeID(int employeeID)
        {
            List<EmployeeSalaryDTO> list = new();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_EmployeeSalaryHistory_GetByEmployeeID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new EmployeeSalaryDTO(
                    (int)reader["SalaryID"],
                    (int)reader["EmployeeID"],
                    (DateTime)reader["EffectiveDate"],
                    (decimal)reader["BaseSalary"],
                    (decimal)reader["Allowances"],
                    (decimal)reader["Deductions"],
                    reader["Notes"]?.ToString()
                ));
            }
            return list;
        }
    }

}

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
    public class clsStudentFeeData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int studentID, int stageFeeItemID, DateTime dueDate, string status, string notes, decimal totalPaid)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentFee_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@StageFeeItemID", stageFeeItemID);
            cmd.Parameters.AddWithValue("@DueDate", dueDate);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TotalPaid", totalPaid);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int studentFeeID, int studentID, int stageFeeItemID, DateTime dueDate, string status, string notes, decimal totalPaid)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentFee_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StudentFeeID", studentFeeID);
            cmd.Parameters.AddWithValue("@StudentID", studentID);
            cmd.Parameters.AddWithValue("@StageFeeItemID", stageFeeItemID);
            cmd.Parameters.AddWithValue("@DueDate", dueDate);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TotalPaid", totalPaid);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int studentFeeID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentFee_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StudentFeeID", studentFeeID);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static StudentFeeDTO GetByID(int studentFeeID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentFee_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StudentFeeID", studentFeeID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new StudentFeeDTO(
                    (int)reader["StudentFeeID"],
                    (int)reader["StudentID"],
                    (int)reader["StageFeeItemID"],
                    (DateTime)reader["DueDate"],
                    reader["Status"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                    (decimal)reader["TotalPaid"]
                );
            }
            return null;
        }

        public static List<StudentFeeDTO> GetAll()
        {
            List<StudentFeeDTO> list = new();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentFee_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StudentFeeDTO(
                    (int)reader["StudentFeeID"],
                    (int)reader["StudentID"],
                    (int)reader["StageFeeItemID"],
                    (DateTime)reader["DueDate"],
                    reader["Status"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                    (decimal)reader["TotalPaid"]
                ));
            }
            return list;
        }

        public static List<StudentFeeDTO> GetByStudentID(int studentID)
        {
            List<StudentFeeDTO> list = new();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentFee_GetByStudentID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StudentID", studentID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StudentFeeDTO(
                    (int)reader["StudentFeeID"],
                    (int)reader["StudentID"],
                    (int)reader["StageFeeItemID"],
                    (DateTime)reader["DueDate"],
                    reader["Status"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                    (decimal)reader["TotalPaid"]
                ));
            }
            return list;
        }

        public static List<StudentFeeDTO> GetUnpaid()
        {
            List<StudentFeeDTO> list = new();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentFee_GetUnpaid", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StudentFeeDTO(
                    (int)reader["StudentFeeID"],
                    (int)reader["StudentID"],
                    (int)reader["StageFeeItemID"],
                    (DateTime)reader["DueDate"],
                    reader["Status"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                    (decimal)reader["TotalPaid"]
                ));
            }
            return list;
        }
    }

}
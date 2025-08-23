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
    public class clsStudentPaymentData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int studentFeeID, DateTime paymentDate, decimal amountPaid, string paymentMethod, string notes)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentPayment_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@StudentFeeID", studentFeeID);
            cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);
            cmd.Parameters.AddWithValue("@AmountPaid", amountPaid);
            cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int paymentID, int studentFeeID, DateTime paymentDate, decimal amountPaid, string paymentMethod, string notes)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentPayment_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@PaymentID", paymentID);
            cmd.Parameters.AddWithValue("@StudentFeeID", studentFeeID);
            cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);
            cmd.Parameters.AddWithValue("@AmountPaid", amountPaid);
            cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int paymentID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentPayment_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@PaymentID", paymentID);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static StudentPaymentDTO GetByID(int paymentID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentPayment_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@PaymentID", paymentID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new StudentPaymentDTO(
                    (int)reader["PaymentID"],
                    (int)reader["StudentFeeID"],
                    (DateTime)reader["PaymentDate"],
                    (decimal)reader["AmountPaid"],
                    reader["PaymentMethod"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                );
            }
            return null;
        }

        public static List<StudentPaymentDTO> GetAll()
        {
            List<StudentPaymentDTO> list = new List<StudentPaymentDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentPayment_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StudentPaymentDTO(
                    (int)reader["PaymentID"],
                    (int)reader["StudentFeeID"],
                    (DateTime)reader["PaymentDate"],
                    (decimal)reader["AmountPaid"],
                    reader["PaymentMethod"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }

        public static List<StudentPaymentDTO> GetByStudentFeeID(int studentFeeID)
        {
            List<StudentPaymentDTO> list = new List<StudentPaymentDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_StudentPayment_GetByStudentFeeID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@StudentFeeID", studentFeeID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new StudentPaymentDTO(
                    (int)reader["PaymentID"],
                    (int)reader["StudentFeeID"],
                    (DateTime)reader["PaymentDate"],
                    (decimal)reader["AmountPaid"],
                    reader["PaymentMethod"].ToString(),
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }
    }
}

using GlobalLayer;
using Microsoft.Data.SqlClient;
using Models.Students;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Students
{
    public class clsStudentDisciplineData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int studentID, DateTime disciplineDate, int points, string notes)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentDiscipline_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@DisciplineDate", disciplineDate);
                cmd.Parameters.AddWithValue("@Points", points);
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

        public static bool Update(int disciplineID, int studentID, DateTime disciplineDate, int points, string notes)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentDiscipline_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@DisciplineID", disciplineID);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@DisciplineDate", disciplineDate);
                cmd.Parameters.AddWithValue("@Points", points);
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

        public static bool Delete(int disciplineID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentDiscipline_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@DisciplineID", disciplineID);
                conn.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
                return false;
            }
        }

        public static StudentDisciplineDTO GetByID(int disciplineID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentDiscipline_GetByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@DisciplineID", disciplineID);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new StudentDisciplineDTO(
                        (int)reader["DisciplineID"],
                        (int)reader["StudentID"],
                        (DateTime)reader["DisciplineDate"],
                        (int)reader["Points"],
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

        public static List<StudentDisciplineDTO> GetAll()
        {
            List<StudentDisciplineDTO> list = new();
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                using SqlCommand cmd = new SqlCommand("sp_StudentDiscipline_GetAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StudentDisciplineDTO(
                        (int)reader["DisciplineID"],
                        (int)reader["StudentID"],
                        (DateTime)reader["DisciplineDate"],
                        (int)reader["Points"],
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
    }

}
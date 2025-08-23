using GlobalLayer;
using Microsoft.Data.SqlClient;
using Models.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.General
{
    public class clsDaysOfWeekData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;
        public static bool UpdateDay(string dayName, bool isWorkingDay)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_DayOfWeek_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@DayName", dayName);
            cmd.Parameters.AddWithValue("@IsWorkingDay", isWorkingDay);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static List<DayOfWeekDTO> GetAllDays()
        {
            List<DayOfWeekDTO> list = new();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_DayOfWeek_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new DayOfWeekDTO(
                    reader["DayName"].ToString(),
                    (bool)reader["IsWorkingDay"]
                ));
            }

            return list;
        }

        public static List<DayOfWeekDTO> GetWorkingDays()
        {
            List<DayOfWeekDTO> list = new();
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_DayOfWeek_GetWorkingDays", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new DayOfWeekDTO(
                    reader["DayName"].ToString(),
                    (bool)reader["IsWorkingDay"]
                ));
            }

            return list;
        }

        public static DayOfWeekDTO GetDayByName(string dayName)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_DayOfWeek_GetByName", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@DayName", dayName);
            conn.Open();

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new DayOfWeekDTO(
                    reader["DayName"].ToString(),
                    (bool)reader["IsWorkingDay"]
                );
            }

            return null;
        }

    }

}

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
    public class clsScheduleData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;

        public static int Insert(int classID, int subjectID, int teacherID, int roomID, string dayOfWeek, TimeSpan startTime,
                                 TimeSpan endTime, int academicYearID, string notes)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Schedule_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ClassID", classID);
            cmd.Parameters.AddWithValue("@SubjectID", subjectID);
            cmd.Parameters.AddWithValue("@TeacherID", teacherID);
            cmd.Parameters.AddWithValue("@RoomID", roomID);
            cmd.Parameters.AddWithValue("@DayOfWeek", dayOfWeek);
            cmd.Parameters.AddWithValue("@StartTime", startTime);
            cmd.Parameters.AddWithValue("@EndTime", endTime);
            cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool Update(int scheduleID, int classID, int subjectID, int teacherID, int roomID, string dayOfWeek,
                                  TimeSpan startTime, TimeSpan endTime, int academicYearID, string notes)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Schedule_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ScheduleID", scheduleID);
            cmd.Parameters.AddWithValue("@ClassID", classID);
            cmd.Parameters.AddWithValue("@SubjectID", subjectID);
            cmd.Parameters.AddWithValue("@TeacherID", teacherID);
            cmd.Parameters.AddWithValue("@RoomID", roomID);
            cmd.Parameters.AddWithValue("@DayOfWeek", dayOfWeek);
            cmd.Parameters.AddWithValue("@StartTime", startTime);
            cmd.Parameters.AddWithValue("@EndTime", endTime);
            cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
            cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(int scheduleID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Schedule_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ScheduleID", scheduleID);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public static ScheduleDTO GetByID(int scheduleID)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Schedule_GetByID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ScheduleID", scheduleID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new ScheduleDTO(
                    (int)reader["ScheduleID"],
                    (int)reader["ClassID"],
                    (int)reader["SubjectID"],
                    (int)reader["TeacherID"],
                    (int)reader["RoomID"],
                    reader["DayOfWeek"].ToString(),
                    (TimeSpan)reader["StartTime"],
                    (TimeSpan)reader["EndTime"],
                    (int)reader["AcademicYearID"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                );
            }
            return null;
        }

        public static List<ScheduleDTO> GetAll()
        {
            List<ScheduleDTO> list = new List<ScheduleDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Schedule_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ScheduleDTO(
                    (int)reader["ScheduleID"],
                    (int)reader["ClassID"],
                    (int)reader["SubjectID"],
                    (int)reader["TeacherID"],
                    (int)reader["RoomID"],
                    reader["DayOfWeek"].ToString(),
                    (TimeSpan)reader["StartTime"],
                    (TimeSpan)reader["EndTime"],
                    (int)reader["AcademicYearID"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }

        public static List<ScheduleDTO> GetByClassID(int classID)
        {
            List<ScheduleDTO> list = new List<ScheduleDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Schedule_GetByClassID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ClassID", classID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ScheduleDTO(
                    (int)reader["ScheduleID"],
                    (int)reader["ClassID"],
                    (int)reader["SubjectID"],
                    (int)reader["TeacherID"],
                    (int)reader["RoomID"],
                    reader["DayOfWeek"].ToString(),
                    (TimeSpan)reader["StartTime"],
                    (TimeSpan)reader["EndTime"],
                    (int)reader["AcademicYearID"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }

        public static List<ScheduleDTO> GetByTeacherID(int teacherID)
        {
            List<ScheduleDTO> list = new List<ScheduleDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Schedule_GetByTeacherID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@TeacherID", teacherID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ScheduleDTO(
                    (int)reader["ScheduleID"],
                    (int)reader["ClassID"],
                    (int)reader["SubjectID"],
                    (int)reader["TeacherID"],
                    (int)reader["RoomID"],
                    reader["DayOfWeek"].ToString(),
                    (TimeSpan)reader["StartTime"],
                    (TimeSpan)reader["EndTime"],
                    (int)reader["AcademicYearID"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }

        public static List<ScheduleDTO> GetByAcademicYearID(int academicYearID)
        {
            List<ScheduleDTO> list = new List<ScheduleDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Schedule_GetByAcademicYearID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ScheduleDTO(
                    (int)reader["ScheduleID"],
                    (int)reader["ClassID"],
                    (int)reader["SubjectID"],
                    (int)reader["TeacherID"],
                    (int)reader["RoomID"],
                    reader["DayOfWeek"].ToString(),
                    (TimeSpan)reader["StartTime"],
                    (TimeSpan)reader["EndTime"],
                    (int)reader["AcademicYearID"],
                    reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                ));
            }
            return list;
        }
    }

}

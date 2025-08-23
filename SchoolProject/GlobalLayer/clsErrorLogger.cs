using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalLayer
{

    public class clsErrorLogger
    {
        private static readonly string _connectionString = DataAccessSettings.ConnectionString;
        private static readonly string _eventSource = "SchoolDB";
        //private static readonly string _logName = "Application";

        static clsErrorLogger()
        {
            try
            {
                if (!EventLog.SourceExists(_eventSource))
                {
                    EventLog.CreateEventSource(_eventSource, "Application");
                }
                else
                {
                    Console.WriteLine("Event Source already exists.");
                }
            }
            catch (Exception ex)
            {
                    Console.WriteLine(ex.Message);
            }
        }

        public static void LogError(Exception ex)
        {
            LogToEventViewer(ex);
            LogToDatabase(ex);
        }

        private static void LogToEventViewer(Exception ex)
        {
            try
            {
                string message = $"Message: {ex.Message}\nSource: {ex.Source ?? "N/A"}\nStackTrace: {ex.StackTrace ?? "N/A"}";
                EventLog.WriteEntry(_eventSource, message, EventLogEntryType.Error);
            }
            catch (Exception innerEx)
            {
                Console.WriteLine(innerEx.Message);
            }
        }
        private static void LogToDatabase(Exception ex)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("InsertErrorLog", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, -1).Value = ex.Message;
                    cmd.Parameters.Add("@StackTrace", SqlDbType.NVarChar, -1).Value = (object)ex.StackTrace ?? DBNull.Value;
                    cmd.Parameters.Add("@Source", SqlDbType.NVarChar, 500).Value = (object)ex.Source ?? DBNull.Value;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception innerEx)
            {
                Console.WriteLine(innerEx.Message);
            }
        }
    }
}

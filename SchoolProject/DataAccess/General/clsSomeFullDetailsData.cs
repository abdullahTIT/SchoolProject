using GlobalLayer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Models.General.clsSomeFullDetailsDTO;

namespace DataAccess.General
{
    public class clsSomeFullDetailsData
    {
        static string _connectionString = DataAccessSettings.ConnectionString;
        public static List<vw_StudentFullInfoDTO> GetAllStudentsFullInfo()
        {
            List<vw_StudentFullInfoDTO> list = new List<vw_StudentFullInfoDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_StudentFullInfo";  // قراءة من الـ View مباشرة

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new vw_StudentFullInfoDTO(
                        (int)reader["StudentID"],
                        reader["FullName"].ToString(),
                        reader["Gender"] == DBNull.Value ? null : reader["Gender"].ToString(),
                        reader["BirthDate"] == DBNull.Value ? null : (DateTime)reader["BirthDate"],
                        reader["Phone"] == DBNull.Value ? null : reader["Phone"].ToString(),
                        reader["Email"] == DBNull.Value ? null : reader["Email"].ToString(),
                        reader["Address"] == DBNull.Value ? null : reader["Address"].ToString(),
                        reader["PersonIsActive"] == DBNull.Value ? null : reader["PersonIsActive"].ToString(),
                        reader["IdentifierValue"] == DBNull.Value ? null : reader["IdentifierValue"].ToString(),
                        reader["IdentifierTypeName"] == DBNull.Value ? null : reader["IdentifierTypeName"].ToString(),
                        reader["EnrollmentDate"] == DBNull.Value ? null : (DateTime)reader["EnrollmentDate"],
                        reader["StageName"] == DBNull.Value ? null : reader["StageName"].ToString(),
                        reader["DocumentPath"] == DBNull.Value ? null : reader["DocumentPath"].ToString(),
                        reader["StudentNotes"] == DBNull.Value ? null : reader["StudentNotes"].ToString(),
                        reader["StudentIsActive"] == DBNull.Value ? null : reader["StudentIsActive"].ToString(),
                        reader["GuardianFullName"] == DBNull.Value ? null : reader["GuardianFullName"].ToString(),
                        reader["GuardianJob"] == DBNull.Value ? null : reader["GuardianJob"].ToString(),
                        reader["GuardianNotes"] == DBNull.Value ? null : reader["GuardianNotes"].ToString(),
                        reader["FeeTypeName"] == DBNull.Value ? null : reader["FeeTypeName"].ToString(),
                        reader["DueDate"] == DBNull.Value ? null : (DateTime)reader["DueDate"],
                        reader["FeeStatus"] == DBNull.Value ? null : reader["FeeStatus"].ToString(),
                        reader["FeeNotes"] == DBNull.Value ? null : reader["FeeNotes"].ToString(),
                        reader["TotalFeePaid"] == DBNull.Value ? 0 : (decimal)reader["TotalFeePaid"],
                        reader["TotalAmountPaid"] == DBNull.Value ? 0 : (decimal)reader["TotalAmountPaid"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_EmployeeFullDetailsDTO> GetAllEmployeeDetails()
        {
            List<vw_EmployeeFullDetailsDTO> list = new List<vw_EmployeeFullDetailsDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_EmployeeFullDetails";  // قراءة من الـ View مباشرة

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new vw_EmployeeFullDetailsDTO(
                        (int)reader["EmployeeID"],
                        reader["FullName"].ToString(),
                        reader["IdentifierValue"] == DBNull.Value ? null : reader["IdentifierValue"].ToString(),
                        reader["Phone"] == DBNull.Value ? null : reader["Phone"].ToString(),
                        reader["Email"] == DBNull.Value ? null : reader["Email"].ToString(),
                        reader["Address"] == DBNull.Value ? null : reader["Address"].ToString(),
                        reader["JobTitleName"] == DBNull.Value ? null : reader["JobTitleName"].ToString(),
                        reader["JobDescription"] == DBNull.Value ? null : reader["JobDescription"].ToString(),
                        reader["IsTeaching"].ToString(),
                        reader["IsAdministrative"].ToString(),
                        reader["CanTeach"].ToString(),
                        reader["RequiresCertification"].ToString(),
                        reader["HireDate"] == DBNull.Value ? null : (DateTime)reader["HireDate"],
                        reader["TerminationDate"] == DBNull.Value ? null : (DateTime)reader["TerminationDate"],
                        reader["EmployeeIsActive"].ToString(),
                        reader["Salary"] == DBNull.Value ? 0 : (decimal)reader["Salary"],
                        reader["EmployeeNotes"] == DBNull.Value ? null : reader["EmployeeNotes"].ToString(),
                        reader["Gender"].ToString(),
                        reader["BirthDate"] == DBNull.Value ? null : (DateTime)reader["BirthDate"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_EmployeeMonthlySalaryDTO> GetAllEmployeeMonthlySalaries()
        {
            List<vw_EmployeeMonthlySalaryDTO> list = new();
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("SELECT * FROM vw_EmployeeMonthlySalary", conn);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new vw_EmployeeMonthlySalaryDTO(
                    (int)reader["EmployeeID"],
                    reader["FullName"].ToString(),
                    (DateTime)reader["SalaryMonth"],
                    (decimal)reader["BaseSalary"],
                    (int)reader["AbsenceDays"],
                    (decimal)reader["DeductionPerAbsent"],
                    (decimal)reader["NetSalary"],
                    (bool)reader["Paid"],
                    reader["PaymentDate"] as DateTime?
                ));
            }
            return list;
        }
        public static List<vw_EmployeeMonthlyAttendanceDTO> GetAllEmployeeMonthlyAttendance()
        {
            List<vw_EmployeeMonthlyAttendanceDTO> list = new();
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("SELECT * FROM vw_EmployeeMonthlyAttendance", conn);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new vw_EmployeeMonthlyAttendanceDTO(
                    (int)reader["EmployeeID"],
                    reader["FullName"].ToString(),
                    reader["SalaryMonth"].ToString(),
                    (int)reader["PresentDays"]
                ));
            }
            return list;
        }
        public static List<vw_FullEmployeeMonthlySalaryDetailsDTO> GetAllFullEmployeeMonthlySalaryDetails()
        {
            List<vw_FullEmployeeMonthlySalaryDetailsDTO> list = new();
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("SELECT * FROM vw_FullEmployeeMonthlySalaryDetails", conn);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new vw_FullEmployeeMonthlySalaryDetailsDTO(
                    (int)reader["EmployeeID"],
                    reader["FullName"].ToString(),
                    (DateTime)reader["SalaryMonth"],
                    (decimal)reader["BaseSalary"],
                    (int)reader["AbsenceDays"],
                    (decimal)reader["DeductionPerAbsent"],
                    (decimal)reader["TotalDeduction"],
                    (decimal)reader["NetSalary"],
                    (bool)reader["Paid"],
                    reader["PaymentDate"] as DateTime?
                ));
            }
            return list;
        }
        public static List<vw_UnpaidEmployeeDTO> GetAllUnpaidEmployees()
        {
            List<vw_UnpaidEmployeeDTO> list = new();
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("SELECT * FROM vw_UnpaidEmployees", conn);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new vw_UnpaidEmployeeDTO(
                    (int)reader["EmployeeID"],
                    reader["FullName"].ToString(),
                    (DateTime)reader["SalaryMonth"],
                    (decimal)reader["BaseSalary"],
                    (decimal)reader["NetSalary"],
                    (bool)reader["Paid"]
                ));
            }
            return list;
        }
        public static List<vw_EmployeeMonthlyAbsenceDTO> GetAllEmployeeMonthlyAbsence()
        {
            List<vw_EmployeeMonthlyAbsenceDTO> list = new();
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("SELECT * FROM vw_EmployeeMonthlyAbsence", conn);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new vw_EmployeeMonthlyAbsenceDTO(
                    (int)reader["EmployeeID"],
                    reader["FullName"].ToString(),
                    reader["SalaryMonth"].ToString(),
                    (int)reader["AbsenceDays"]
                ));
            }
            return list;
        }
        public static List<vw_TerminatedEmployeeDTO> GetAllTerminatedEmployees()
        {
            List<vw_TerminatedEmployeeDTO> list = new();
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("SELECT * FROM vw_TerminatedEmployees", conn);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new vw_TerminatedEmployeeDTO(
                    (int)reader["EmployeeID"],
                    reader["FullName"].ToString(),
                    (int)reader["JobTitleID"],
                    (DateTime)reader["HireDate"],
                    reader["TerminationDate"] as DateTime?,
                    reader["Notes"]?.ToString()
                ));
            }
            return list;
        }
        public static List<vw_EmployeeAnnualSalarySummaryDTO> GetAllEmployeeAnnualSalarySummaries()
        {
            List<vw_EmployeeAnnualSalarySummaryDTO> list = new();
            using SqlConnection conn = new(_connectionString);
            using SqlCommand cmd = new("SELECT * FROM vw_EmployeeAnnualSalarySummary", conn);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new vw_EmployeeAnnualSalarySummaryDTO(
                    (int)reader["EmployeeID"],
                    reader["FullName"].ToString(),
                    (int)reader["SalaryYear"],
                    (decimal)reader["TotalBaseSalary"],
                    (decimal)reader["TotalDeductions"],
                    (decimal)reader["TotalNetSalary"]
                ));
            }
            return list;
        }
        public static List<vw_CurrentEmployeeSalaryDTO> GetAllCurrentEmployeeSalaries()
        {
            List<vw_CurrentEmployeeSalaryDTO> list = new List<vw_CurrentEmployeeSalaryDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_CurrentEmployeeSalary";  // قراءة من الـ View مباشرة

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new vw_CurrentEmployeeSalaryDTO(
                        (int)reader["EmployeeID"],
                        reader["FullName"].ToString(),
                        reader["BaseSalary"] == DBNull.Value ? 0 : (decimal)reader["BaseSalary"],
                        reader["Allowances"] == DBNull.Value ? 0 : (decimal)reader["Allowances"],
                        reader["Deductions"] == DBNull.Value ? 0 : (decimal)reader["Deductions"],
                        (DateTime)reader["EffectiveDate"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_StudentFeeStatusDTO> GetAllStudentFeeStatus()
        {
            List<vw_StudentFeeStatusDTO> list = new List<vw_StudentFeeStatusDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_StudentFeeStatus";

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new vw_StudentFeeStatusDTO(
                        (int)reader["StudentFeeID"],
                        (int)reader["StudentID"],
                        reader["FullName"] == DBNull.Value ? null : reader["FullName"].ToString(),
                        reader["FeeName"] == DBNull.Value ? null : reader["FeeName"].ToString(),
                        reader["TotalRequired"] == DBNull.Value ? 0 : (decimal)reader["TotalRequired"],
                        reader["TotalPaid"] == DBNull.Value ? 0 : (decimal)reader["TotalPaid"],
                        reader["Remaining"] == DBNull.Value ? 0 : (decimal)reader["Remaining"],
                        reader["PaymentStatus"] == DBNull.Value ? null : reader["PaymentStatus"].ToString(),
                        reader["DueDate"] == DBNull.Value ? null : (DateTime)reader["DueDate"],
                        reader["Status"] == DBNull.Value ? null : reader["Status"].ToString(),
                        reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_StudentFinancialStatusDTO> GetAllStudentFinancialStatus()
        {
            List<vw_StudentFinancialStatusDTO> list = new List<vw_StudentFinancialStatusDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_StudentFinancialStatus";

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new vw_StudentFinancialStatusDTO(
                        (int)reader["StudentID"],
                        (int)reader["StudentFeeID"],
                        reader["FeeName"] == DBNull.Value ? null : reader["FeeName"].ToString(),
                        reader["TotalFee"] == DBNull.Value ? 0 : (decimal)reader["TotalFee"],
                        reader["TotalPaid"] == DBNull.Value ? 0 : (decimal)reader["TotalPaid"],
                        reader["Remaining"] == DBNull.Value ? 0 : (decimal)reader["Remaining"],
                        reader["TotalInstallments"] == DBNull.Value ? 0 : (int)reader["TotalInstallments"],
                        reader["PaidInstallments"] == DBNull.Value ? 0 : (int)reader["PaidInstallments"],
                        reader["UnpaidInstallments"] == DBNull.Value ? 0 : (int)reader["UnpaidInstallments"],
                        reader["LastDueDate"] == DBNull.Value ? null : (DateTime)reader["LastDueDate"],
                        reader["PaymentStatus"] == DBNull.Value ? null : reader["PaymentStatus"].ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_UpcomingInstallmentsDTO> GetAllUpcomingInstallments()
        {
            List<vw_UpcomingInstallmentsDTO> list = new List<vw_UpcomingInstallmentsDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_UpcomingInstallments";

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new vw_UpcomingInstallmentsDTO(
                        (int)reader["InstallmentID"],
                        (int)reader["StudentFeeID"],
                        (int)reader["StudentID"],
                        reader["FullName"] == DBNull.Value ? null : reader["FullName"].ToString(),
                        (DateTime)reader["DueDate"],
                        reader["Amount"] == DBNull.Value ? 0 : (decimal)reader["Amount"],
                        (bool)reader["IsPaid"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_WeeklyRoomScheduleDTO> GetAllWeeklyRoomSchedules()
        {
            List<vw_WeeklyRoomScheduleDTO> list = new List<vw_WeeklyRoomScheduleDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_WeeklyRoomSchedule";

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new vw_WeeklyRoomScheduleDTO(
                        reader["RoomName"].ToString(),
                        reader["ClassName"].ToString(),
                        reader["SubjectName"].ToString(),
                        (int)reader["EmployeeID"],
                        (string)reader["DayOfWeek"],
                        (TimeSpan)reader["StartTime"],
                        (TimeSpan)reader["EndTime"],
                        (int)reader["AcademicYearID"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_WeeklyRoomScheduleDTO> GetWeeklyRoomScheduleByDay(string dayOfWeek)
        {
            List<vw_WeeklyRoomScheduleDTO> list = new List<vw_WeeklyRoomScheduleDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_WeeklyRoomSchedule WHERE DayOfWeek = @DayOfWeek";

                using SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DayOfWeek", dayOfWeek);

                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new vw_WeeklyRoomScheduleDTO(
                        reader["RoomName"].ToString(),
                        reader["ClassName"].ToString(),
                        reader["SubjectName"].ToString(),
                        (int)reader["EmployeeID"],
                        (string)reader["DayOfWeek"],
                        (TimeSpan)reader["StartTime"],
                        (TimeSpan)reader["EndTime"],
                        (int)reader["AcademicYearID"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_DailyRoomScheduleDTO> GetAllDailyRoomSchedules()
        {
            List<vw_DailyRoomScheduleDTO> list = new List<vw_DailyRoomScheduleDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_DailyRoomSchedule";

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new vw_DailyRoomScheduleDTO(
                        reader["RoomName"].ToString(),
                        reader["ClassName"].ToString(),
                        reader["SubjectName"].ToString(),
                        (string)reader["DayOfWeek"],
                        reader["StartTime"].ToString(),
                        reader["EndTime"].ToString(),
                        (int)reader["AcademicYearID"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_WeeklyClassScheduleDTO> GetAllWeeklyClassSchedules()
        {
            List<vw_WeeklyClassScheduleDTO> list = new List<vw_WeeklyClassScheduleDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_WeeklyClassSchedule";

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new vw_WeeklyClassScheduleDTO(
                        reader["ClassName"].ToString(),
                        reader["SubjectName"].ToString(),
                        (int)reader["TeacherID"],
                        reader["RoomName"].ToString(),
                        (string)reader["DayOfWeek"],
                        reader["StartTime"].ToString(),
                        reader["EndTime"].ToString(),
                        (int)reader["AcademicYearID"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_WeeklyClassScheduleDTO> GetWeeklyClassScheduleByDay(string dayOfWeek)
        {
            List<vw_WeeklyClassScheduleDTO> list = new List<vw_WeeklyClassScheduleDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_WeeklyClassSchedule WHERE DayOfWeek = @DayOfWeek";

                using SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DayOfWeek", dayOfWeek);

                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new vw_WeeklyClassScheduleDTO(
                        reader["ClassName"].ToString(),
                        reader["SubjectName"].ToString(),
                        (int)reader["TeacherID"],
                        reader["RoomName"].ToString(),
                        (string)reader["DayOfWeek"],
                        reader["StartTime"].ToString(),
                        reader["EndTime"].ToString(),
                        (int)reader["AcademicYearID"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_AvailableRoomsNowDTO> GetAvailableRoomsNow()
        {
            List<vw_AvailableRoomsNowDTO> list = new List<vw_AvailableRoomsNowDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_AvailableRoomsNow";

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new vw_AvailableRoomsNowDTO(
                        (int)reader["RoomID"],
                        reader["RoomName"].ToString(),
                        (int)reader["RoomTypeID"],
                        (int)reader["Capacity"],
                        reader["Location"].ToString(),
                        (bool)reader["IsAvailable"],
                        (bool)reader["IsReservable"],
                        reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_TeacherSchedulesDTO> GetAllTeacherSchedules()
        {
            List<vw_TeacherSchedulesDTO> list = new List<vw_TeacherSchedulesDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_TeacherSchedules";

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new vw_TeacherSchedulesDTO(
                        (int)reader["EmployeeID"],
                        reader["FullName"].ToString(),
                        reader["ClassName"].ToString(),
                        reader["SubjectName"].ToString(),
                        reader["RoomName"].ToString(),
                        (string)reader["DayOfWeek"],
                        (TimeSpan)reader["StartTime"],
                        (TimeSpan)reader["EndTime"],
                        (int)reader["AcademicYearID"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_StudentFinalGradesDTO> GetAllStudentFinalGrades()
        {
            List<vw_StudentFinalGradesDTO> list = new List<vw_StudentFinalGradesDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_StudentFinalGrades";

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new vw_StudentFinalGradesDTO(
                        (int)reader["StudentID"],
                        reader["FullName"].ToString(),
                        reader["StageName"].ToString(),
                        reader["SubjectName"].ToString(),
                        reader["ExamTypeName"].ToString(),
                        (decimal)reader["Grade"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_StudentAverageGradesDTO> GetAllStudentAverageGrades()
        {
            List<vw_StudentAverageGradesDTO> list = new List<vw_StudentAverageGradesDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_StudentAverageGrades";

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new vw_StudentAverageGradesDTO(
                        (int)reader["StudentID"],
                        reader["FullName"].ToString(),
                        reader["StageName"].ToString(),
                        reader["AcademicYearName"].ToString(),
                        reader["ExamTypeName"].ToString(),
                        (decimal)reader["AverageGrade"],
                        (int)reader["SubjectsCount"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_TopStudentPerStageYearDTO> GetTopStudentsPerStageYear()
        {
            List<vw_TopStudentPerStageYearDTO> list = new List<vw_TopStudentPerStageYearDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_TopStudentPerStageYear";

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new vw_TopStudentPerStageYearDTO(
                        (int)reader["StudentID"],
                        reader["FullName"].ToString(),
                        reader["StageName"].ToString(),
                        reader["AcademicYearName"].ToString(),
                        (decimal)reader["AverageGrade"],
                        (int)reader["RankInStage"]
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_StudentPassFailStatusDTO> GetStudentPassFailStatus()
        {
            List<vw_StudentPassFailStatusDTO> list = new List<vw_StudentPassFailStatusDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = "SELECT * FROM vw_StudentPassFailStatus";

                using SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new vw_StudentPassFailStatusDTO(
                        (int)reader["StudentID"],
                        reader["FullName"].ToString(),
                        reader["StageName"].ToString(),
                        reader["AcademicYearName"].ToString(),
                        (decimal)reader["AverageGrade"],
                        reader["FinalStatus"].ToString()
                    ));
                }
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex);
            }

            return list;
        }
        public static List<vw_StudentPassFailStatusDTO> GetStudentPassFailStatus(string stageName, string academicYearName)
        {
            List<vw_StudentPassFailStatusDTO> list = new List<vw_StudentPassFailStatusDTO>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = @"
            SELECT * 
            FROM vw_StudentPassFailStatus
            WHERE StageName = @StageName AND AcademicYearName = @AcademicYearName";

                using SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StageName", stageName);
                cmd.Parameters.AddWithValue("@AcademicYearName", academicYearName);

                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new vw_StudentPassFailStatusDTO(
                        (int)reader["StudentID"],
                        reader["FullName"].ToString(),
                        reader["StageName"].ToString(),
                        reader["AcademicYearName"].ToString(),
                        (decimal)reader["AverageGrade"],
                        reader["FinalStatus"].ToString()
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

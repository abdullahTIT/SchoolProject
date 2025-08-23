using DataAccess.General;
using GlobalLayer;
using Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.General
{
    public class clsSomeFullDetails
    {
        public static List<clsSomeFullDetailsDTO.vw_StudentFullInfoDTO> GetAllStudnets()
        {
            return clsSomeFullDetailsData.GetAllStudentsFullInfo();
        }
        public static List<clsSomeFullDetailsDTO.vw_EmployeeFullDetailsDTO> GetAllEmployees()
        {
            return clsSomeFullDetailsData.GetAllEmployeeDetails();
        }
        public static List<clsSomeFullDetailsDTO.vw_EmployeeMonthlySalaryDTO> GetAllEmployeeMonthlySalaries()
        {
            return clsSomeFullDetailsData.GetAllEmployeeMonthlySalaries();
        }
        public static List<clsSomeFullDetailsDTO. vw_EmployeeMonthlyAttendanceDTO> GetAllEmployeeMonthlyAttendance()
        {
            return clsSomeFullDetailsData.GetAllEmployeeMonthlyAttendance();
        }
        public static List<clsSomeFullDetailsDTO.vw_FullEmployeeMonthlySalaryDetailsDTO> GetAllFullEmployeeMonthlySalaryDetails()
        {
            return clsSomeFullDetailsData.GetAllFullEmployeeMonthlySalaryDetails();
        }
        public static List<clsSomeFullDetailsDTO.vw_UnpaidEmployeeDTO> GetAllUnpaidEmployees()
        {
            return clsSomeFullDetailsData.GetAllUnpaidEmployees();
        }
        public static List<clsSomeFullDetailsDTO.vw_EmployeeMonthlyAbsenceDTO> GetAllEmployeeMonthlyAbsence()
        {
            return clsSomeFullDetailsData.GetAllEmployeeMonthlyAbsence();
        }
        public static List<clsSomeFullDetailsDTO.vw_TerminatedEmployeeDTO> GetAllTerminatedEmployees()
        {
            return clsSomeFullDetailsData.GetAllTerminatedEmployees();
        }
        public static List<clsSomeFullDetailsDTO.vw_EmployeeAnnualSalarySummaryDTO> GetAllEmployeeAnnualSalarySummaries()
        {
            return clsSomeFullDetailsData.GetAllEmployeeAnnualSalarySummaries();
        }
        public static List<clsSomeFullDetailsDTO.vw_CurrentEmployeeSalaryDTO> GetAllEmployeeSalaries()
        {
            return clsSomeFullDetailsData.GetAllCurrentEmployeeSalaries();
        }
        public static List<clsSomeFullDetailsDTO.vw_StudentFeeStatusDTO> GetAllFeesStatus()
        {
            return clsSomeFullDetailsData.GetAllStudentFeeStatus();
        }
        public static List<clsSomeFullDetailsDTO.vw_StudentFinancialStatusDTO> GetAllFinancialStatus()
        {
            return clsSomeFullDetailsData.GetAllStudentFinancialStatus();
        }
        public static List<clsSomeFullDetailsDTO.vw_UpcomingInstallmentsDTO> GetUpcomingInstallments()
        {
            return clsSomeFullDetailsData.GetAllUpcomingInstallments();
        }
        public static List<clsSomeFullDetailsDTO.vw_WeeklyRoomScheduleDTO> GetAllWeeklySchedules()
        {
            return clsSomeFullDetailsData.GetAllWeeklyRoomSchedules();
        }
        public static List<clsSomeFullDetailsDTO.vw_WeeklyRoomScheduleDTO> GetWeeklySchedulesByDay(string dayOfWeek)
        {
            return clsSomeFullDetailsData.GetWeeklyRoomScheduleByDay(dayOfWeek);
        }
        public static List<clsSomeFullDetailsDTO.vw_DailyRoomScheduleDTO> GetAllDailySchedules()
        {
            return clsSomeFullDetailsData.GetAllDailyRoomSchedules();
        }
        public static List<clsSomeFullDetailsDTO.vw_WeeklyClassScheduleDTO> GetAllWeeklyClassSchedules()
        {
            return clsSomeFullDetailsData.GetAllWeeklyClassSchedules();
        }
        public static List<clsSomeFullDetailsDTO.vw_WeeklyClassScheduleDTO> GetWeeklyClassSchedulesByDay(string dayOfWeek)
        {
            return clsSomeFullDetailsData.GetWeeklyClassScheduleByDay(dayOfWeek);
        }
        public static List<clsSomeFullDetailsDTO.vw_AvailableRoomsNowDTO> GetAvailableRoomsNow()
        {
            return clsSomeFullDetailsData.GetAvailableRoomsNow();
        }
        public static List<clsSomeFullDetailsDTO.vw_TeacherSchedulesDTO> GetAllTeacherSchedules()
        {
            return clsSomeFullDetailsData.GetAllTeacherSchedules();
        }
        public static List<clsSomeFullDetailsDTO.vw_StudentFinalGradesDTO> GetStudentFinalGrades()
        {
            return clsSomeFullDetailsData.GetAllStudentFinalGrades();
        }
        public static List<clsSomeFullDetailsDTO.vw_StudentAverageGradesDTO> GetStudentAverages()
        {
            return clsSomeFullDetailsData.GetAllStudentAverageGrades();
        }
        public static List<clsSomeFullDetailsDTO.vw_TopStudentPerStageYearDTO> GetTopStudents()
        {
            return clsSomeFullDetailsData.GetTopStudentsPerStageYear();
        }
        public static List<clsSomeFullDetailsDTO.vw_StudentPassFailStatusDTO> getStudentPassFailStatus()
        {
            return clsSomeFullDetailsData.GetStudentPassFailStatus();
        }
        public static List<clsSomeFullDetailsDTO.vw_StudentPassFailStatusDTO> getStudentPassFailStatus(string stageName, string academicYearName)
        {
            return clsSomeFullDetailsData.GetStudentPassFailStatus(stageName, academicYearName);
        }


    }
}

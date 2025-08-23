using DataAccess.Emloyees;
using System;
using System.Collections.Generic;
using System.Linq;
using GlobalLayer;
using System.Text;
using System.Threading.Tasks;
using Models.Employees;


namespace Business.Employees
{
    public class clsEmployeeAttendance
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int AttendanceID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public string Status { get; set; }

        public clsEmployeeAttendance() { }

        public clsEmployeeAttendance(EmployeeAttendanceDTO dto, enMode mode = enMode.Update)
        {
            AttendanceID = dto.AttendanceID;
            EmployeeID = dto.EmployeeID;
            AttendanceDate = dto.AttendanceDate;
            CheckInTime = dto.CheckInTime;
            CheckOutTime = dto.CheckOutTime;
            Status = dto.Status;
            Mode = mode;
        }

        private bool _AddNew()
        {
            if (clsEmployeeAttendanceData.IsAttendanceExists(EmployeeID, AttendanceDate))
            {
                Console.WriteLine("Attendance already exists for this employee on this date.");
                return false;
            }

            AttendanceID = clsEmployeeAttendanceData.InsertAttendance(EmployeeID, AttendanceDate, CheckInTime, CheckOutTime, Status);
            return AttendanceID != -1;
        }

        private bool _Update()
        {
            return clsEmployeeAttendanceData.UpdateAttendance(AttendanceID, EmployeeID, AttendanceDate, CheckInTime, CheckOutTime, Status);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _Update();
                default:
                    return false;
            }
        }

        public static bool Delete(int attendanceID, out int rowsAffected)
        {
            return clsEmployeeAttendanceData.DeleteAttendance(attendanceID, out rowsAffected);
        }

        public static clsEmployeeAttendance Find(int attendanceID)
        {
            var dto = clsEmployeeAttendanceData.GetAttendanceByID(attendanceID);
            if (dto == null) return null;
            return new clsEmployeeAttendance(dto, enMode.Update);
        }

        public static List<EmployeeAttendanceDTO> GetAll()
        {
            return clsEmployeeAttendanceData.GetAllAttendances();
        }


        public static List<EmployeeAttendanceDTO> GetByEmployeeID(int employeeID)
        {
            return clsEmployeeAttendanceData.GetAttendanceByEmployeeID(employeeID);
        }

        public static List<EmployeeAttendanceDTO> GetByEmployeeIDBetweenDates(int employeeID, DateTime startDate, DateTime endDate)
        {
            return clsEmployeeAttendanceData.GetAttendanceByEmployeeIDBetweenDates(employeeID, startDate, endDate);
        }

        public static List<EmployeeAttendanceDTO> GetByStatus(string status)
        {
            return clsEmployeeAttendanceData.GetAttendanceByStatus(status);
        }

        public static List<EmployeeAttendanceDTO> GetByStatusBetweenDates(string status, DateTime startDate, DateTime endDate)
        {
            return clsEmployeeAttendanceData.GetAttendanceByStatusBetweenDates(status, startDate, endDate);
        }
    }
}

using DataAccess.Students;
using GlobalLayer;
using Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Students
{
    public class clsStudentAttendance
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int AttendanceID { get; set; }
        public int StudentID { get; set; }
        public int ScheduleID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        public clsStudentAttendance() { }

        public clsStudentAttendance(StudentAttendanceDTO dto, enMode mode = enMode.Update)
        {
            AttendanceID = dto.AttendanceID;
            StudentID = dto.StudentID;
            ScheduleID = dto.ScheduleID;
            AttendanceDate = dto.AttendanceDate;
            Status = dto.Status;
            Notes = dto.Notes;
            Mode = mode;
        }

        private bool _AddNew()
        {
            if (clsStudentAttendanceData.DoesAttendanceExist(StudentID, ScheduleID, AttendanceDate))
                return false;


            AttendanceID = clsStudentAttendanceData.Insert(StudentID, ScheduleID, AttendanceDate, Status, Notes);
            return AttendanceID != -1;
        }

        private bool _Update()
        {
            if (clsStudentAttendanceData.DoesAttendanceExist(StudentID, ScheduleID, AttendanceDate))
                return false;


            return clsStudentAttendanceData.Update(AttendanceID, StudentID, ScheduleID, AttendanceDate, Status, Notes);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    return _AddNew();
                case enMode.Update:
                    return _Update();
                default:
                    return false;
            }
        }

        public static clsStudentAttendance Find(int id)
        {
            var dto = clsStudentAttendanceData.GetByID(id);
            return dto == null ? null : new clsStudentAttendance(dto);
        }

        public static bool Delete(int id)
        {
            return clsStudentAttendanceData.Delete(id);
        }

        public static List<StudentAttendanceDTO> GetAll()
        {
            return clsStudentAttendanceData.GetAll();
        }

        public static List<StudentAttendanceDTO> GetByStudentID(int studentID)
        {
            return clsStudentAttendanceData.GetByStudentID(studentID);
        }

        public static List<StudentAttendanceDTO> GetByDateRange(DateTime from, DateTime to)
        {
            return clsStudentAttendanceData.GetByDateRange(from, to);
        }

        public static List<StudentAttendanceDTO> GetByStatus(string status)
        {
            return clsStudentAttendanceData.GetByStatus(status);
        }
    }

}

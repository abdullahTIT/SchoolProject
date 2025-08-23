using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students
{
    public class StudentAttendanceDTO
    {
        public int AttendanceID { get; set; }
        public int StudentID { get; set; }
        public int ScheduleID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        public StudentAttendanceDTO() { }

        public StudentAttendanceDTO(int attendanceID, int studentID, int scheduleID, DateTime attendanceDate, string status, string notes)
        {
            AttendanceID = attendanceID;
            StudentID = studentID;
            ScheduleID = scheduleID;
            AttendanceDate = attendanceDate;
            Status = status;
            Notes = notes;
        }
    }

}

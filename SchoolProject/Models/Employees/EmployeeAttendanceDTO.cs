using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Employees
{
    public class EmployeeAttendanceDTO
    {
        public int AttendanceID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public string Status { get; set; }

        public EmployeeAttendanceDTO() { }

        public EmployeeAttendanceDTO(int attendanceID, int employeeID, DateTime attendanceDate, TimeSpan? checkInTime, TimeSpan? checkOutTime, string status)
        {
            AttendanceID = attendanceID;
            EmployeeID = employeeID;
            AttendanceDate = attendanceDate;
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
            Status = status;
        }
    }

}

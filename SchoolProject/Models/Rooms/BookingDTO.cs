using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Rooms
{
    public class BookingDTO
    {
        public int BookingID { get; set; }
        public int RoomID { get; set; }
        public int EmployeeID { get; set; }
        public string Purpose { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string RecurrenceType { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }

        public BookingDTO() { }

        public BookingDTO(int bookingID, int roomID, int employeeID, string purpose, DateTime bookingDate,
                          DateTime startTime, DateTime endTime, string status, string notes,
                          string recurrenceType, DateTime? recurrenceEndDate)
        {
            BookingID = bookingID;
            RoomID = roomID;
            EmployeeID = employeeID;
            Purpose = purpose;
            BookingDate = bookingDate;
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
            Notes = notes;
            RecurrenceType = recurrenceType;
            RecurrenceEndDate = recurrenceEndDate;
        }
    }

}

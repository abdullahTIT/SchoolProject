using DataAccess.Rooms;
using GlobalLayer;
using Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;  
using System.Text;
using System.Threading.Tasks;

namespace Business.Rooms
{
    public class clsBooking
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

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

        public clsBooking() { }

        public clsBooking(BookingDTO dto, enMode mode = enMode.Update)
        {
            BookingID = dto.BookingID;
            RoomID = dto.RoomID;
            EmployeeID = dto.EmployeeID;
            Purpose = dto.Purpose;
            BookingDate = dto.BookingDate;
            StartTime = dto.StartTime;
            EndTime = dto.EndTime;
            Status = dto.Status;
            Notes = dto.Notes;
            RecurrenceType = dto.RecurrenceType;
            RecurrenceEndDate = dto.RecurrenceEndDate;
            Mode = mode;
        }

        private bool _AddNew()
        {
            BookingID = clsBookingData.Insert(RoomID, EmployeeID, Purpose, BookingDate, StartTime, EndTime, Status, Notes, RecurrenceType, RecurrenceEndDate);
            return BookingID != -1;
        }

        private bool _Update()
        {
            return clsBookingData.Update(BookingID, RoomID, EmployeeID, Purpose, BookingDate, StartTime, EndTime, Status, Notes, RecurrenceType, RecurrenceEndDate);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int bookingID)
        {
            return clsBookingData.Delete(bookingID);
        }

        public static clsBooking Find(int bookingID)
        {
            var dto = clsBookingData.GetByID(bookingID);
            return dto == null ? null : new clsBooking(dto);
        }

        public static List<BookingDTO> GetAll()
        {
            return clsBookingData.GetAll();
        }

        public static List<BookingDTO> GetByRoom(int roomID)
        {
            return clsBookingData.GetByRoomID(roomID);
        }

        public static List<BookingDTO> GetByEmployee(int employeeID)
        {
            return clsBookingData.GetByEmployeeID(employeeID);
        }
    }


}

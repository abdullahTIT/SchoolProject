using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Rooms
{
    public class RoomDTO
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public int RoomTypeID { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsReservable { get; set; }
        public string Notes { get; set; }

        public RoomDTO() { }

        public RoomDTO(int roomID, string roomName, int roomTypeID, int capacity, string location,
                       bool isAvailable, bool isReservable, string notes)
        {
            RoomID = roomID;
            RoomName = roomName;
            RoomTypeID = roomTypeID;
            Capacity = capacity;
            Location = location;
            IsAvailable = isAvailable;
            IsReservable = isReservable;
            Notes = notes;
        }
    }

}

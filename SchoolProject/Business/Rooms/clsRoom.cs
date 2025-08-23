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
    public class clsRoom
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public int RoomTypeID { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsReservable { get; set; }
        public string Notes { get; set; }

        public clsRoom() { }

        public clsRoom(RoomDTO dto, enMode mode = enMode.Update)
        {
            RoomID = dto.RoomID;
            RoomName = dto.RoomName;
            RoomTypeID = dto.RoomTypeID;
            Capacity = dto.Capacity;
            Location = dto.Location;
            IsAvailable = dto.IsAvailable;
            IsReservable = dto.IsReservable;
            Notes = dto.Notes;
            Mode = mode;
        }

        private bool _AddNew()
        {
            RoomID = clsRoomData.Insert(RoomName, RoomTypeID, Capacity, Location, IsAvailable, IsReservable, Notes);
            return RoomID != -1;
        }

        private bool _Update()
        {
            return clsRoomData.Update(RoomID, RoomName, RoomTypeID, Capacity, Location, IsAvailable, IsReservable, Notes);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int roomID)
        {
            return clsRoomData.Delete(roomID);
        }

        public static clsRoom Find(int roomID)
        {
            var dto = clsRoomData.GetByID(roomID);
            return dto == null ? null : new clsRoom(dto);
        }

        public static List<RoomDTO> GetAll()
        {
            return clsRoomData.GetAll();
        }

        public static List<RoomDTO> GetAvailable()
        {
            return clsRoomData.GetAvailableRooms();
        }
        public static List<RoomDTO> GetAvailableRoomsAtTime(DateTime targetDateTime)
        {
            return clsRoomData.GetAvailableRoomsAtTime(targetDateTime);
        }

    }

}

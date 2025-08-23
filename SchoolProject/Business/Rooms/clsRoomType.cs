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
    public class clsRoomType
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int RoomTypeID { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public clsRoomType() { }

        public clsRoomType(RoomTypeDTO dto, enMode mode = enMode.Update)
        {
            RoomTypeID = dto.RoomTypeID;
            TypeName = dto.TypeName;
            Description = dto.Description;
            IsActive = dto.IsActive;
            Mode = mode;
        }

        private bool _AddNew()
        {
            RoomTypeID = clsRoomTypeData.Insert(TypeName, Description, IsActive);
            return RoomTypeID != -1;
        }

        private bool _Update()
        {
            return clsRoomTypeData.Update(RoomTypeID, TypeName, Description, IsActive);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int roomTypeID)
        {
            return clsRoomTypeData.Delete(roomTypeID);
        }

        public static clsRoomType Find(int roomTypeID)
        {
            var dto = clsRoomTypeData.GetByID(roomTypeID);
            return dto == null ? null : new clsRoomType(dto);
        }

        public static List<RoomTypeDTO> GetAll()
        {
            return clsRoomTypeData.GetAll();
        }

        public static List<RoomTypeDTO> GetActive()
        {
            return clsRoomTypeData.GetActive();
        }
    }

}

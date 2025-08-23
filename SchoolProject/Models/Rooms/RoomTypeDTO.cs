using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Rooms
{
    public class RoomTypeDTO
    {
        public int RoomTypeID { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public RoomTypeDTO() { }

        public RoomTypeDTO(int id, string typeName, string description, bool isActive)
        {
            RoomTypeID = id;
            TypeName = typeName;
            Description = description;
            IsActive = isActive;
        }
    }

}

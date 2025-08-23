using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Rooms
{
    public class ClassDTO
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int StageID { get; set; }
        public int Capacity { get; set; }
        public string Notes { get; set; }

        public ClassDTO() { }

        public ClassDTO(int classID, string className, int stageID, int capacity, string notes)
        {
            ClassID = classID;
            ClassName = className;
            StageID = stageID;
            Capacity = capacity;
            Notes = notes;
        }
    }

}

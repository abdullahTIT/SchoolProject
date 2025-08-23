using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.General
{
    public class StageDTO
    {
        public int StageID { get; set; }
        public string StageName { get; set; }
        public bool IsActive { get; set; }

        public StageDTO() { }

        public StageDTO(int stageID, string stageName, bool isActive)
        {
            StageID = stageID;
            StageName = stageName;
            IsActive = isActive;
        }
    }

}

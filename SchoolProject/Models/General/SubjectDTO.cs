using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.General
{
    public class SubjectDTO
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int StageID { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }

        public SubjectDTO() { }

        public SubjectDTO(int subjectID, string subjectName, int stageID, bool isActive, string notes)
        {
            SubjectID = subjectID;
            SubjectName = subjectName;
            StageID = stageID;
            IsActive = isActive;
            Notes = notes;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students
{
    public class GuardianDTO
    {
        public int GuardianID { get; set; }
        public int PersonID { get; set; }
        public string Jobs { get; set; }
        public string Notes { get; set; }

        public GuardianDTO() { }

        public GuardianDTO(int guardianID, int personID, string jobs, string notes)
        {
            GuardianID = guardianID;
            PersonID = personID;
            Jobs = jobs;
            Notes = notes;
        }
    }

}

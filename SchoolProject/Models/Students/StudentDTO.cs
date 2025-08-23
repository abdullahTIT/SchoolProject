using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students
{
    public class StudentDTO
    {
        public int StudentID { get; set; }
        public int PersonID { get; set; }
        public int GuardianID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int StageID { get; set; }
        public bool IsActive { get; set; }
        public string DocumentPath { get; set; }
        public string Notes { get; set; }

        public StudentDTO() { }

        public StudentDTO(int studentID, int personID, int guardianID, DateTime enrollmentDate, int stageID, bool isActive, string documentPath, string notes)
        {
            StudentID = studentID;
            PersonID = personID;
            GuardianID = guardianID;
            EnrollmentDate = enrollmentDate;
            StageID = stageID;
            IsActive = isActive;
            DocumentPath = documentPath;
            Notes = notes;
        }
    }

}

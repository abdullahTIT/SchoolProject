using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students
{
    public class StudentStageDTO
    {
        public int StudentStageID { get; set; }
        public int StudentID { get; set; }
        public int StageID { get; set; }
        public int AcademicYearID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; }

        public StudentStageDTO() { }

        public StudentStageDTO(int studentStageID, int studentID, int stageID, int academicYearID, DateTime enrollmentDate, bool isActive)
        {
            StudentStageID = studentStageID;
            StudentID = studentID;
            StageID = stageID;
            AcademicYearID = academicYearID;
            EnrollmentDate = enrollmentDate;
            IsActive = isActive;
        }
    }

}

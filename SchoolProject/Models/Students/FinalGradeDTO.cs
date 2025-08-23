using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students
{
    public class FinalGradeDTO
    {
        public int FinalGradeID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public int StageID { get; set; }
        public int AcademicYearID { get; set; }
        public int ExamPeriodID { get; set; }
        public int ExamTypeID { get; set; }
        public decimal Grade { get; set; }
        public decimal MaxGrade { get; set; }
        public DateTime CalculationDate { get; set; }
        public string Notes { get; set; }

        public FinalGradeDTO() { }

        public FinalGradeDTO(int finalGradeID, int studentID, int subjectID, int stageID, int academicYearID,
            int examPeriodID, int examTypeID, decimal grade, decimal maxGrade, DateTime calculationDate, string notes)
        {
            FinalGradeID = finalGradeID;
            StudentID = studentID;
            SubjectID = subjectID;
            StageID = stageID;
            AcademicYearID = academicYearID;
            ExamPeriodID = examPeriodID;
            ExamTypeID = examTypeID;
            Grade = grade;
            MaxGrade = maxGrade;
            CalculationDate = calculationDate;
            Notes = notes;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students.Exams
{
    public class ExamDTO
    {
        public int ExamID { get; set; }
        public int SubjectID { get; set; }
        public int TeacherID { get; set; }
        public DateTime ExamDate { get; set; }
        public DateTime TakeDate { get; set; }
        public short Grade { get; set; }
        public short MaxGrade { get; set; }
        public string ExamDocument { get; set; }
        public string Notes { get; set; }
        public int ExamPeriodID { get; set; }
        public bool IsActive { get; set; }

        public ExamDTO() { }

        public ExamDTO(int examID, int subjectID, int teacherID, DateTime examDate, DateTime takeDate,
                       short grade, short maxGrade, string examDocument, string notes, int examPeriodID, bool isActive)
        {
            ExamID = examID;
            SubjectID = subjectID;
            TeacherID = teacherID;
            ExamDate = examDate;
            TakeDate = takeDate;
            Grade = grade;
            MaxGrade = maxGrade;
            ExamDocument = examDocument;
            Notes = notes;
            ExamPeriodID = examPeriodID;
            IsActive = isActive;
        }
    }

}

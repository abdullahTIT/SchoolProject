using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students.Exams
{
    public class ExamPeriodDTO
    {
        public int ExamPeriodID { get; set; }
        public string ExamPeriodName { get; set; }
        public int ExamTypeID { get; set; }
        public int AcademicYearID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ExamPeriodDTO() { }

        public ExamPeriodDTO(int examPeriodID, string examPeriodName, int examTypeID, int academicYearID, DateTime startDate, DateTime endDate)
        {
            ExamPeriodID = examPeriodID;
            ExamPeriodName = examPeriodName;
            ExamTypeID = examTypeID;
            AcademicYearID = academicYearID;
            StartDate = startDate;
            EndDate = endDate;
        }
    }

}

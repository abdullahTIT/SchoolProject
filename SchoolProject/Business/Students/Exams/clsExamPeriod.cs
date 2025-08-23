using DataAccess.Students;
using DataAccess.Students.Exams;
using GlobalLayer;
using Models.Students;
using Models.Students.Exams;

namespace Business.Students.Exams
{
    public class clsExamPeriod
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int ExamPeriodID { get; set; }
        public string ExamPeriodName { get; set; }
        public int ExamTypeID { get; set; }
        public int AcademicYearID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public clsExamPeriod() { }

        public clsExamPeriod(ExamPeriodDTO dto, enMode mode = enMode.Update)
        {
            ExamPeriodID = dto.ExamPeriodID;
            ExamPeriodName = dto.ExamPeriodName;
            ExamTypeID = dto.ExamTypeID;
            AcademicYearID = dto.AcademicYearID;
            StartDate = dto.StartDate;
            EndDate = dto.EndDate;
            Mode = mode;
        }

        private bool _AddNew()
        {
            ExamPeriodID = clsExamPeriodData.Insert(ExamPeriodName, ExamTypeID, AcademicYearID, StartDate, EndDate);
            return ExamPeriodID != -1;
        }

        private bool _Update()
        {
            return clsExamPeriodData.Update(ExamPeriodID, ExamPeriodName, ExamTypeID, AcademicYearID, StartDate, EndDate);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int examPeriodID)
        {
            // تحقق من العلاقات أو القيود قبل الحذف إذا احتجت
            return clsExamPeriodData.Delete(examPeriodID);
        }

        public static clsExamPeriod Find(int examPeriodID)
        {
            var dto = clsExamPeriodData.GetByID(examPeriodID);
            return dto == null ? null : new clsExamPeriod(dto);
        }

        public static List<ExamPeriodDTO> GetAll()
        {
            return clsExamPeriodData.GetAll();
        }

        public static List<ExamPeriodDTO> GetByAcademicYear(int academicYearID)
        {
            return clsExamPeriodData.GetByAcademicYearID(academicYearID);
        }
    }

}

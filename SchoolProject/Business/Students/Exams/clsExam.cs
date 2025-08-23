using DataAccess.Students;
using DataAccess.Students.Exams;
using GlobalLayer;
using Models.Students;
using Models.Students.Exams;

namespace Business.Students.Exams
{
    public class clsExam
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

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

        public clsExam() { }

        public clsExam(ExamDTO dto, enMode mode = enMode.Update)
        {
            ExamID = dto.ExamID;
            SubjectID = dto.SubjectID;
            TeacherID = dto.TeacherID;
            ExamDate = dto.ExamDate;
            TakeDate = dto.TakeDate;
            Grade = dto.Grade;
            MaxGrade = dto.MaxGrade;
            ExamDocument = dto.ExamDocument;
            Notes = dto.Notes;
            ExamPeriodID = dto.ExamPeriodID;
            IsActive = dto.IsActive;
            Mode = mode;
        }

        private bool _AddNew()
        {
            ExamID = clsExamData.Insert(SubjectID, TeacherID, ExamDate, TakeDate, Grade, MaxGrade, ExamDocument, Notes, ExamPeriodID, IsActive);
            return ExamID != -1;
        }

        private bool _Update()
        {
            return clsExamData.Update(ExamID, SubjectID, TeacherID, ExamDate, TakeDate, Grade, MaxGrade, ExamDocument, Notes, ExamPeriodID, IsActive);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int examID)
        {
            // يمكنك إضافة تحقق من العلاقات قبل الحذف
            return clsExamData.Delete(examID);
        }

        public static clsExam Find(int examID)
        {
            var dto = clsExamData.GetByID(examID);
            return dto == null ? null : new clsExam(dto);
        }

        public static List<ExamDTO> GetAll()
        {
            return clsExamData.GetAll();
        }

        public static List<ExamDTO> GetActive()
        {
            return clsExamData.GetActive();
        }

        public static List<ExamDTO> GetBySubject(int subjectID)
        {
            return clsExamData.GetBySubjectID(subjectID);
        }
    }

}

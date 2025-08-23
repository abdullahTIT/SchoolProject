using DataAccess.Students;
using DataAccess.Students.Exams;
using GlobalLayer;
using Models.Students;
using Models.Students.Exams;

namespace Business.Students.Exams
{
    public class clsExamType
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int ExamTypeID { get; set; }
        public string ExamTypeName { get; set; }
        public string Description { get; set; }

        public clsExamType() { }

        public clsExamType(ExamTypeDTO dto, enMode mode = enMode.Update)
        {
            ExamTypeID = dto.ExamTypeID;
            ExamTypeName = dto.ExamTypeName;
            Description = dto.Description;
            Mode = mode;
        }

        private bool _AddNew()
        {
            ExamTypeID = clsExamTypeData.Insert(ExamTypeName, Description);
            return ExamTypeID != -1;
        }

        private bool _Update()
        {
            return clsExamTypeData.Update(ExamTypeID, ExamTypeName, Description);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int examTypeID)
        {
            // ممكن تضيف تحقق علاقات أو شروط خاصة قبل الحذف
            return clsExamTypeData.Delete(examTypeID);
        }

        public static clsExamType Find(int examTypeID)
        {
            var dto = clsExamTypeData.GetByID(examTypeID);
            return dto == null ? null : new clsExamType(dto);
        }

        public static List<ExamTypeDTO> GetAll()
        {
            return clsExamTypeData.GetAll();
        }
    }


}

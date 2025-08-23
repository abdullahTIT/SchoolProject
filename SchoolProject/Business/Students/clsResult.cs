using DataAccess.Students;
using GlobalLayer;
using Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Students
{
    public class clsResult
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int ResultID { get; set; }
        public int StudentID { get; set; }
        public int ExamID { get; set; }
        public decimal? Score { get; set; }
        public bool IsAbsent { get; set; }
        public string Notes { get; set; }

        public clsResult() { }

        public clsResult(ResultDTO dto, enMode mode = enMode.Update)
        {
            ResultID = dto.ResultID;
            StudentID = dto.StudentID;
            ExamID = dto.ExamID;
            Score = dto.Score;
            IsAbsent = dto.IsAbsent;
            Notes = dto.Notes;
            Mode = mode;
        }

        private bool _AddNew()
        {
            ResultID = clsResultData.Insert(StudentID, ExamID, Score, IsAbsent, Notes);
            return ResultID != -1;
        }

        private bool _Update()
        {
            return clsResultData.Update(ResultID, StudentID, ExamID, Score, IsAbsent, Notes);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int resultID)
        {
            // ممكن تضيف تحقق علاقات أو شروط خاصة قبل الحذف
            return clsResultData.Delete(resultID);
        }

        public static clsResult Find(int resultID)
        {
            var dto = clsResultData.GetByID(resultID);
            return dto == null ? null : new clsResult(dto);
        }

        public static List<ResultDTO> GetByStudent(int studentID)
        {
            return clsResultData.GetByStudentID(studentID);
        }

        public static List<ResultDTO> GetByExam(int examID)
        {
            return clsResultData.GetByExamID(examID);
        }

        public static List<ResultDTO> GetAll()
        {
            return clsResultData.GetAll();
        }
    }

}

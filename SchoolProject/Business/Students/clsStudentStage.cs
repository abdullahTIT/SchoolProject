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
    public class clsStudentStage
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int StudentStageID { get; set; }
        public int StudentID { get; set; }
        public int StageID { get; set; }
        public int AcademicYearID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; }

        public clsStudentStage() { }

        public clsStudentStage(StudentStageDTO dto, enMode mode = enMode.Update)
        {
            StudentStageID = dto.StudentStageID;
            StudentID = dto.StudentID;
            StageID = dto.StageID;
            AcademicYearID = dto.AcademicYearID;
            EnrollmentDate = dto.EnrollmentDate;
            IsActive = dto.IsActive;
            Mode = mode;
        }

        private bool _AddNew()
        {
            StudentStageID = clsStudentStageData.Insert(StudentID, StageID, AcademicYearID, EnrollmentDate, IsActive);
            return StudentStageID != -1;
        }

        private bool _Update()
        {
            return clsStudentStageData.Update(StudentStageID, StudentID, StageID, AcademicYearID, EnrollmentDate, IsActive);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int studentStageID)
        {
            // إضافة تحقق من علاقات البيانات إذا أردت قبل الحذف
            return clsStudentStageData.Delete(studentStageID);
        }

        public static clsStudentStage Find(int studentStageID)
        {
            var dto = clsStudentStageData.GetByID(studentStageID);
            return dto == null ? null : new clsStudentStage(dto);
        }

        public static List<StudentStageDTO> GetAll()
        {
            return clsStudentStageData.GetAll();
        }

        public static List<StudentStageDTO> GetByStudent(int studentID)
        {
            return clsStudentStageData.GetByStudentID(studentID);
        }
        public bool UpdateIsActive(bool isActive)
        {
            bool result = clsStudentStageData.UpdateIsActive(StudentStageID, isActive);
            if (result)
                IsActive = isActive;
            return result;
        }

        public static List<StudentStageDTO> GetByStage(int stageID)
        {
            return clsStudentStageData.GetByStageID(stageID);
        }

        public static List<StudentStageDTO> GetByAcademicYear(int academicYearID)
        {
            return clsStudentStageData.GetByAcademicYearID(academicYearID);
        }

    }

}

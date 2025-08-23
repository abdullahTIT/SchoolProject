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

    public class clsStudent
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int StudentID { get; set; }
        public int PersonID { get; set; }
        public int GuardianID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int StageID { get; set; }
        public bool IsActive { get; set; }
        public string DocumentPath { get; set; }
        public string Notes { get; set; }

        public clsStudent() { }

        public clsStudent(StudentDTO dto, enMode mode = enMode.Update)
        {
            StudentID = dto.StudentID;
            PersonID = dto.PersonID;
            GuardianID = dto.GuardianID;
            EnrollmentDate = dto.EnrollmentDate;
            StageID = dto.StageID;
            IsActive = dto.IsActive;
            DocumentPath = dto.DocumentPath;
            Notes = dto.Notes;
            Mode = mode;
        }

        private bool _AddNew()
        {
            StudentID = clsStudentData.InsertStudent(PersonID, GuardianID, EnrollmentDate, StageID, IsActive, DocumentPath, Notes);
            return StudentID != -1;
        }

        private bool _Update()
        {
            return clsStudentData.UpdateStudent(StudentID, PersonID, GuardianID, EnrollmentDate, StageID, IsActive, DocumentPath, Notes);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _Update();
                default:
                    return false;
            }
        }

        public static bool Delete(int studentID)
        {
            return clsStudentData.DeleteStudent(studentID);
        }

        public static clsStudent Find(int studentID)
        {
            var dto = clsStudentData.GetStudentByID(studentID);
            if (dto == null) return null;
            return new clsStudent(dto, enMode.Update);
        }

        public static List<StudentDTO> GetAll()
        {
            return clsStudentData.GetAllStudents();
        }

        public bool UpdateIsActive(bool isActive)
        {
            bool result = clsStudentData.UpdateStudentIsActive(StudentID, isActive);
            if (result) IsActive = isActive;
            return result;
        }

        public static List<StudentDTO> GetByStage(int stageID)
        {
            return clsStudentData.GetStudentsByStage(stageID);
        }

        public static List<StudentDTO> GetByGuardianID(int guardianID)
        {
            return clsStudentData.GetStudentsByGuardianID(guardianID);
        }

        public static int GetEnrollmentCount()
        {
            return clsStudentData.GetStudentsEnrollmentCount();
        }

        public static List<StudentDTO> GetEnrollmentList()
        {
            return clsStudentData.GetStudentsEnrollment();
        }

        public static int GetEnrollmentCountInStage(int stageID)
        {
            return clsStudentData.GetStudentsEnrollmentCountInStage(stageID);
        }

        public static List<StudentDTO> GetEnrollmentListInStage(int stageID)
        {
            return clsStudentData.GetStudentsEnrollmentInStage(stageID);
        }

        public static int GetEnrollmentCountByDate(DateTime startDate, DateTime endDate)
        {
            return clsStudentData.GetStudentsEnrollmentCountByDate(startDate, endDate);
        }

        public static List<StudentDTO> GetEnrollmentListByDate(DateTime startDate, DateTime endDate)
        {
            return clsStudentData.GetStudentsEnrollmentByDate(startDate, endDate);
        }

        public static int GetEnrollmentCountInStageByDate(int stageID, DateTime startDate, DateTime endDate)
        {
            return clsStudentData.GetStudentsEnrollmentCountInStageByDate(stageID, startDate, endDate);
        }

        public static List<StudentDTO> GetEnrollmentListInStageByDate(int stageID, DateTime startDate, DateTime endDate)
        {
            return clsStudentData.GetStudentsEnrollmentInStageByDate(stageID, startDate, endDate);
        }
    }

}

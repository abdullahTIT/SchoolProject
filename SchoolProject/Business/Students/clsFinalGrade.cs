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
    public class clsFinalGrade
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

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

        public clsFinalGrade() { }

        public clsFinalGrade(FinalGradeDTO dto, enMode mode = enMode.AddNew)
        {
            FinalGradeID = dto.FinalGradeID;
            StudentID = dto.StudentID;
            SubjectID = dto.SubjectID;
            StageID = dto.StageID;
            AcademicYearID = dto.AcademicYearID;
            ExamPeriodID = dto.ExamPeriodID;
            ExamTypeID = dto.ExamTypeID;
            Grade = dto.Grade;
            MaxGrade = dto.MaxGrade;
            CalculationDate = dto.CalculationDate;
            Notes = dto.Notes;
            Mode = mode;
        }
        public static clsFinalGrade Find(int finalGradeID)
        {
            var dto = clsFinalGradesData.GetByID(finalGradeID);
            if (dto == null) return null;
            return new clsFinalGrade(dto, enMode.Update);
        }

        public static List<FinalGradeDTO> GetAll()
        {
            return clsFinalGradesData.GetAll();
        }

    }
}



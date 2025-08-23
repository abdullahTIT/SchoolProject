using DataAccess.General;
using GlobalLayer;
using Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.General
{
    public class clsAcademicYear
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int AcademicYearID { get; set; }
        public string AcademicYearName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public clsAcademicYear() { }

        public clsAcademicYear(AcademicYearDTO dto, enMode mode = enMode.Update)
        {
            AcademicYearID = dto.AcademicYearID;
            AcademicYearName = dto.AcademicYearName;
            StartDate = dto.StartDate;
            EndDate = dto.EndDate;
            Mode = mode;
        }

        private bool _AddNew()
        {
            AcademicYearID = clsAcademicYearData.Insert(AcademicYearName, StartDate, EndDate);
            return AcademicYearID != -1;
        }

        private bool _Update()
        {
            return clsAcademicYearData.Update(AcademicYearID, AcademicYearName, StartDate, EndDate);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int academicYearID)
        {
            // ممكن إضافة تحقق قبل الحذف لو كان مرتبط ببيانات أخرى
            return clsAcademicYearData.Delete(academicYearID);
        }

        public static clsAcademicYear Find(int academicYearID)
        {
            var dto = clsAcademicYearData.GetByID(academicYearID);
            return dto == null ? null : new clsAcademicYear(dto);
        }

        public static List<AcademicYearDTO> GetAll()
        {
            return clsAcademicYearData.GetAll();
        }
    }

}

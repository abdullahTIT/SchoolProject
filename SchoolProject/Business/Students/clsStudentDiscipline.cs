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
    public class clsStudentDiscipline
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int DisciplineID { get; set; }
        public int StudentID { get; set; }
        public DateTime DisciplineDate { get; set; }
        public int Points { get; set; }
        public string Notes { get; set; }

        public clsStudentDiscipline() { }

        public clsStudentDiscipline(StudentDisciplineDTO dto, enMode mode = enMode.Update)
        {
            DisciplineID = dto.DisciplineID;
            StudentID = dto.StudentID;
            DisciplineDate = dto.DisciplineDate;
            Points = dto.Points;
            Notes = dto.Notes;
            Mode = mode;
        }

        private bool _AddNew()
        {
            DisciplineID = clsStudentDisciplineData.Insert(StudentID, DisciplineDate, Points, Notes);
            return DisciplineID != -1;
        }

        private bool _Update()
        {
            return clsStudentDisciplineData.Update(DisciplineID, StudentID, DisciplineDate, Points, Notes);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static clsStudentDiscipline Find(int id)
        {
            var dto = clsStudentDisciplineData.GetByID(id);
            return dto == null ? null : new clsStudentDiscipline(dto);
        }

        public static bool Delete(int id)
        {
            return clsStudentDisciplineData.Delete(id);
        }

        public static List<StudentDisciplineDTO> GetAll()
        {
            return clsStudentDisciplineData.GetAll();
        }
    }

}

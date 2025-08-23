using DataAccess.Students;
using DataAccess.Students.Money;
using GlobalLayer;
using Models.Students.Money;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Students.Money
{
    public class clsStudentFee
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int StudentFeeID { get; set; }
        public int StudentID { get; set; }
        public int StageFeeItemID { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public decimal TotalPaid { get; set; }

        public clsStudentFee() { }

        public clsStudentFee(StudentFeeDTO dto, enMode mode = enMode.Update)
        {
            StudentFeeID = dto.StudentFeeID;
            StudentID = dto.StudentID;
            StageFeeItemID = dto.StageFeeItemID;
            DueDate = dto.DueDate;
            Status = dto.Status;
            Notes = dto.Notes;
            TotalPaid = dto.TotalPaid;
            Mode = mode;
        }

        private bool _AddNew()
        {
            StudentFeeID = clsStudentFeeData.Insert(StudentID, StageFeeItemID, DueDate, Status, Notes, TotalPaid);
            return StudentFeeID != -1;
        }

        private bool _Update()
        {
            return clsStudentFeeData.Update(StudentFeeID, StudentID, StageFeeItemID, DueDate, Status, Notes, TotalPaid);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int studentFeeID)
        {
            return clsStudentFeeData.Delete(studentFeeID);
        }

        public static clsStudentFee Find(int studentFeeID)
        {
            var dto = clsStudentFeeData.GetByID(studentFeeID);
            return dto == null ? null : new clsStudentFee(dto);
        }

        public static List<StudentFeeDTO> GetAll()
        {
            return clsStudentFeeData.GetAll();
        }

        public static List<StudentFeeDTO> GetByStudent(int studentID)
        {
            return clsStudentFeeData.GetByStudentID(studentID);
        }

        public static List<StudentFeeDTO> GetUnpaid()
        {
            return clsStudentFeeData.GetUnpaid();
        }
    }

}

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
    public class clsStudentInstallment
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int InstallmentID { get; set; }
        public int StudentFeeID { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public bool? IsPaid { get; set; }
        public string Notes { get; set; }

        public clsStudentInstallment() { }

        public clsStudentInstallment(StudentInstallmentDTO dto, enMode mode = enMode.Update)
        {
            InstallmentID = dto.InstallmentID;
            StudentFeeID = dto.StudentFeeID;
            DueDate = dto.DueDate;
            Amount = dto.Amount;
            IsPaid = dto.IsPaid;
            Notes = dto.Notes;
            Mode = mode;
        }

        private bool _AddNew()
        {
            InstallmentID = clsStudentInstallmentData.Insert(StudentFeeID, DueDate, Amount, IsPaid, Notes);
            return InstallmentID != -1;
        }

        private bool _Update()
        {
            return clsStudentInstallmentData.Update(InstallmentID, StudentFeeID, DueDate, Amount, IsPaid, Notes);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static clsStudentInstallment Find(int id)
        {
            var dto = clsStudentInstallmentData.GetByID(id);
            return dto == null ? null : new clsStudentInstallment(dto);
        }

        public static bool Delete(int id)
        {
            return clsStudentInstallmentData.Delete(id);
        }

        public static List<StudentInstallmentDTO> GetAll()
        {
            return clsStudentInstallmentData.GetAll();
        }

        public static List<StudentInstallmentDTO> GetUnpaid()
        {
            return clsStudentInstallmentData.GetUnpaid();
        }

        public static List<StudentInstallmentDTO> GetByStudentID(int studentID)
        {
            return clsStudentInstallmentData.GetByStudentID(studentID);
        }

        public static List<StudentInstallmentDTO> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return clsStudentInstallmentData.GetByDateRange(startDate, endDate);
        }

        public static decimal GetTotalRemainingAmount()
        {
            return clsStudentInstallmentData.GetTotalRemainingAmount();
        }
    }

}

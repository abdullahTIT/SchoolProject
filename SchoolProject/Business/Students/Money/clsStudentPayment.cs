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
    public class clsStudentPayment
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int PaymentID { get; set; }
        public int StudentFeeID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
        public string Notes { get; set; }

        public clsStudentPayment() { }

        public clsStudentPayment(StudentPaymentDTO dto, enMode mode = enMode.Update)
        {
            PaymentID = dto.PaymentID;
            StudentFeeID = dto.StudentFeeID;
            PaymentDate = dto.PaymentDate;
            AmountPaid = dto.AmountPaid;
            PaymentMethod = dto.PaymentMethod;
            Notes = dto.Notes;
            Mode = mode;
        }

        private bool _AddNew()
        {
            PaymentID = clsStudentPaymentData.Insert(StudentFeeID, PaymentDate, AmountPaid, PaymentMethod, Notes);
            return PaymentID != -1;
        }

        private bool _Update()
        {
            return clsStudentPaymentData.Update(PaymentID, StudentFeeID, PaymentDate, AmountPaid, PaymentMethod, Notes);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int paymentID)
        {
            return clsStudentPaymentData.Delete(paymentID);
        }

        public static clsStudentPayment Find(int paymentID)
        {
            var dto = clsStudentPaymentData.GetByID(paymentID);
            return dto == null ? null : new clsStudentPayment(dto);
        }

        public static List<StudentPaymentDTO> GetAll()
        {
            return clsStudentPaymentData.GetAll();
        }

        public static List<StudentPaymentDTO> GetByStudentFee(int studentFeeID)
        {
            return clsStudentPaymentData.GetByStudentFeeID(studentFeeID);
        }
    }

}

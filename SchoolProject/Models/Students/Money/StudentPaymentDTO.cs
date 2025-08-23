using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students.Money
{
    public class StudentPaymentDTO
    {
        public int PaymentID { get; set; }
        public int StudentFeeID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
        public string Notes { get; set; }

        public StudentPaymentDTO() { }

        public StudentPaymentDTO(int paymentID, int studentFeeID, DateTime paymentDate, decimal amountPaid, string paymentMethod, string notes)
        {
            PaymentID = paymentID;
            StudentFeeID = studentFeeID;
            PaymentDate = paymentDate;
            AmountPaid = amountPaid;
            PaymentMethod = paymentMethod;
            Notes = notes;
        }
    }

}

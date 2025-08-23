using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students.Money
{
    public class StudentInstallmentDTO
    {
        public int InstallmentID { get; set; }
        public int StudentFeeID { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public bool? IsPaid { get; set; }
        public string Notes { get; set; }

        public StudentInstallmentDTO() { }

        public StudentInstallmentDTO(int installmentID, int studentFeeID, DateTime dueDate, decimal amount, bool? isPaid, string notes)
        {
            InstallmentID = installmentID;
            StudentFeeID = studentFeeID;
            DueDate = dueDate;
            Amount = amount;
            IsPaid = isPaid;
            Notes = notes;
        }
    }

}

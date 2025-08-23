using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students.Money
{
    public class StudentFeeDTO
    {
        public int StudentFeeID { get; set; }
        public int StudentID { get; set; }
        public int StageFeeItemID { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public decimal TotalPaid { get; set; }

        public StudentFeeDTO() { }

        public StudentFeeDTO(int id, int studentID, int stageFeeItemID, DateTime dueDate, string status, string notes, decimal totalPaid)
        {
            StudentFeeID = id;
            StudentID = studentID;
            StageFeeItemID = stageFeeItemID;
            DueDate = dueDate;
            Status = status;
            Notes = notes;
            TotalPaid = totalPaid;
        }
    }

}

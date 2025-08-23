using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Employees
{
    public class DeductionDTO
    {
        public int DeductionID { get; set; }
        public int EmployeeID { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public DateTime AppliedInSalaryMonth { get; set; }

        public DeductionDTO() { }

        public DeductionDTO(int deductionID, int employeeID, decimal amount, string reason, DateTime date, DateTime appliedInSalaryMonth)
        {
            DeductionID = deductionID;
            EmployeeID = employeeID;
            Amount = amount;
            Reason = reason;
            Date = date;
            AppliedInSalaryMonth = appliedInSalaryMonth;
        }
    }

}

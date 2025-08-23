using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Employees
{
    public class BonusDTO
    {
        public int BonusID { get; set; }
        public int EmployeeID { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public DateTime AppliedInSalaryMonth { get; set; }

        public BonusDTO() { }

        public BonusDTO(int bonusID, int employeeID, decimal amount, string reason, DateTime date, DateTime appliedInSalaryMonth)
        {
            BonusID = bonusID;
            EmployeeID = employeeID;
            Amount = amount;
            Reason = reason;
            Date = date;
            AppliedInSalaryMonth = appliedInSalaryMonth;
        }
    }

}

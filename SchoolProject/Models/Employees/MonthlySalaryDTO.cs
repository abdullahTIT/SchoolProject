using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Employees
{
    public class MonthlySalaryDTO
    {
        public int SalaryID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime SalaryMonth { get; set; }
        public decimal BaseSalary { get; set; }
        public int AbsenceDays { get; set; }
        public decimal DeductionPerAbsent { get; set; }
        public decimal NetSalary { get; } // محسوب تلقائياً
        public bool Paid { get; set; }
        public DateTime? PaymentDate { get; set; }

        public MonthlySalaryDTO() { }

        public MonthlySalaryDTO(int salaryID, int employeeID, DateTime salaryMonth, decimal baseSalary,
                                int absenceDays, decimal deductionPerAbsent, bool paid, DateTime? paymentDate)
        {
            SalaryID = salaryID;
            EmployeeID = employeeID;
            SalaryMonth = salaryMonth;
            BaseSalary = baseSalary;
            AbsenceDays = absenceDays;
            DeductionPerAbsent = deductionPerAbsent;
            Paid = paid;
            PaymentDate = paymentDate;
        }
    }

}

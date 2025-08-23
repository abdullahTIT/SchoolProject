using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Employees
{
    public class EmployeeSalaryDTO
    {
        public int SalaryID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }
        public string Notes { get; set; }

        public EmployeeSalaryDTO() { }

        public EmployeeSalaryDTO(int salaryID, int employeeID, DateTime effectiveDate, decimal baseSalary, decimal allowances, decimal deductions, string notes)
        {
            SalaryID = salaryID;
            EmployeeID = employeeID;
            EffectiveDate = effectiveDate;
            BaseSalary = baseSalary;
            Allowances = allowances;
            Deductions = deductions;
            Notes = notes;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Employees
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public int PersonID { get; set; }
        public int JobTitleID { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public decimal Salary { get; set; }

        public EmployeeDTO() { }

        public EmployeeDTO(int employeeID, int personID, int jobTitleID, DateTime hireDate,
                           DateTime? terminationDate, bool isActive, string notes, decimal salary)
        {
            EmployeeID = employeeID;
            PersonID = personID;
            JobTitleID = jobTitleID;
            HireDate = hireDate;
            TerminationDate = terminationDate;
            IsActive = isActive;
            Notes = notes;
            Salary = salary;
        }
    }

}

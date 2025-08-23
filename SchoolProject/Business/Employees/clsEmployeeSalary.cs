using DataAccess.Emloyees;
using System;
using System.Collections.Generic;
using System.Linq;
using GlobalLayer;
using System.Text;
using System.Threading.Tasks;
using Models.Employees;


namespace Business.Employees
{
    public class clsEmployeeSalary
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int SalaryID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }
        public string Notes { get; set; }

        public clsEmployeeSalary() { }

        public clsEmployeeSalary(EmployeeSalaryDTO dto, enMode mode = enMode.Update)
        {
            SalaryID = dto.SalaryID;
            EmployeeID = dto.EmployeeID;
            EffectiveDate = dto.EffectiveDate;
            BaseSalary = dto.BaseSalary;
            Allowances = dto.Allowances;
            Deductions = dto.Deductions;
            Notes = dto.Notes;
            Mode = mode;
        }

        private bool _AddNew()
        {
            SalaryID = clsEmployeeSalaryData.Insert(EmployeeID, EffectiveDate, BaseSalary, Allowances, Deductions, Notes);
            return SalaryID != -1;
        }

        private bool _Update()
        {
            return clsEmployeeSalaryData.Update(SalaryID, EmployeeID, EffectiveDate, BaseSalary, Allowances, Deductions, Notes);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int salaryID)
        {
            return clsEmployeeSalaryData.Delete(salaryID);
        }

        public static clsEmployeeSalary Find(int salaryID)
        {
            var dto = clsEmployeeSalaryData.GetByID(salaryID);
            return dto != null ? new clsEmployeeSalary(dto) : null;
        }

        public static List<EmployeeSalaryDTO> GetAll()
        {
            return clsEmployeeSalaryData.GetAll();
        }

        public static List<EmployeeSalaryDTO> GetByEmployee(int employeeID)
        {
            return clsEmployeeSalaryData.GetByEmployeeID(employeeID);
        }
    }

}

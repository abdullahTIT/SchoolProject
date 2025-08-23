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
    public class clsDeduction
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int DeductionID { get; set; }
        public int EmployeeID { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public DateTime AppliedInSalaryMonth { get; set; }

        public clsDeduction() { }

        public clsDeduction(DeductionDTO dto, enMode mode = enMode.Update)
        {
            DeductionID = dto.DeductionID;
            EmployeeID = dto.EmployeeID;
            Amount = dto.Amount;
            Reason = dto.Reason;
            Date = dto.Date;
            AppliedInSalaryMonth = dto.AppliedInSalaryMonth;
            Mode = mode;
        }

        private bool _AddNew()
        {
            DeductionID = clsDeductionData.Insert(EmployeeID, Amount, Reason, Date, AppliedInSalaryMonth);
            return DeductionID != -1;
        }

        private bool _Update()
        {
            return clsDeductionData.Update(DeductionID, EmployeeID, Amount, Reason, Date, AppliedInSalaryMonth);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int deductionID)
        {
            return clsDeductionData.Delete(deductionID);
        }

        public static clsDeduction Find(int deductionID)
        {
            var dto = clsDeductionData.GetByID(deductionID);
            if (dto == null) return null;
            return new clsDeduction(dto);
        }

        public static List<DeductionDTO> GetAll()
        {
            return clsDeductionData.GetAll();
        }

        public static List<DeductionDTO> GetByEmployee(int employeeID)
        {
            return clsDeductionData.GetByEmployeeID(employeeID);
        }
        // حساب مجموع الخصومات لموظف خلال فترة محددة
        public static decimal GetTotalByEmployeeInRange(int employeeID, DateTime fromDate, DateTime toDate)
        {
            return clsDeductionData.GetTotalDeductionsByEmployeeInRange(employeeID, fromDate, toDate);
        }

        // جلب الخصومات حسب شهر الراتب
        public static List<DeductionDTO> GetBySalaryMonth(DateTime salaryMonth)
        {
            return clsDeductionData.GetDeductionsBySalaryMonth(salaryMonth);
        }

        // بحث مرن في الخصومات حسب شروط اختيارية
        public static List<DeductionDTO> Search(int? employeeID = null, DateTime? fromDate = null, DateTime? toDate = null, decimal? minAmount = null, decimal? maxAmount = null)
        {
            return clsDeductionData.SearchDeductions(employeeID, fromDate, toDate, minAmount, maxAmount);
        }

    }

}

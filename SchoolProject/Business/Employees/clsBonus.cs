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
    public class clsBonus
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int BonusID { get; set; }
        public int EmployeeID { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public DateTime AppliedInSalaryMonth { get; set; }

        public clsBonus() { }

        public clsBonus(BonusDTO dto, enMode mode = enMode.Update)
        {
            BonusID = dto.BonusID;
            EmployeeID = dto.EmployeeID;
            Amount = dto.Amount;
            Reason = dto.Reason;
            Date = dto.Date;
            AppliedInSalaryMonth = dto.AppliedInSalaryMonth;
            Mode = mode;
        }

        private bool _AddNew()
        {
            BonusID = clsBonusData.Insert(EmployeeID, Amount, Reason, Date, AppliedInSalaryMonth);
            return BonusID != -1;
        }

        private bool _Update()
        {
            return clsBonusData.Update(BonusID, EmployeeID, Amount, Reason, Date, AppliedInSalaryMonth);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int bonusID)
        {
            return clsBonusData.Delete(bonusID);
        }

        public static clsBonus Find(int bonusID)
        {
            var dto = clsBonusData.GetByID(bonusID);
            if (dto == null) return null;
            return new clsBonus(dto);
        }

        public static List<BonusDTO> GetAll()
        {
            return clsBonusData.GetAll();
        }

        public static List<BonusDTO> GetByEmployee(int employeeID)
        {
            return clsBonusData.GetByEmployeeID(employeeID);
        }
        // حساب مجموع المكافآت لموظف خلال فترة زمنية
        public static decimal GetTotalBonusForEmployeeInPeriod(int employeeID, DateTime startDate, DateTime endDate)
        {
            return clsBonusData.GetTotalAmountByEmployeeAndPeriod(employeeID, startDate, endDate);
        }

        // جلب المكافآت لموظف معين حسب AppliedInSalaryMonth
        public static List<BonusDTO> GetBonusesBySalaryMonth(int employeeID, DateTime salaryMonth)
        {
            return clsBonusData.GetByAppliedSalaryMonth(employeeID, salaryMonth);
        }

    }

}

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
    public class clsMonthlySalary
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int SalaryID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime SalaryMonth { get; set; }
        public decimal BaseSalary { get; set; }
        public int AbsenceDays { get; set; }
        public decimal DeductionPerAbsent { get; set; }
        public decimal NetSalary { get; }
        public bool Paid { get; set; }
        public DateTime? PaymentDate { get; set; }

        public clsMonthlySalary() { }

        public clsMonthlySalary(MonthlySalaryDTO dto, enMode mode = enMode.AddNew)
        {
            SalaryID = dto.SalaryID;
            EmployeeID = dto.EmployeeID;
            SalaryMonth = dto.SalaryMonth;
            BaseSalary = dto.BaseSalary;
            AbsenceDays = dto.AbsenceDays;
            DeductionPerAbsent = dto.DeductionPerAbsent;
            Paid = dto.Paid;
            PaymentDate = dto.PaymentDate;
            Mode = mode;
        }

        private bool _AddNew()
        {
            int newID = clsMonthlySalaryData.Insert(EmployeeID, SalaryMonth, BaseSalary, AbsenceDays, DeductionPerAbsent, Paid, PaymentDate);
            if (newID > 0)
            {
                SalaryID = newID;
                Mode = enMode.Update;
                return true;
            }
            return false;
        }

        private bool _Update()
        {
            return clsMonthlySalaryData.Update(SalaryID, EmployeeID, SalaryMonth, BaseSalary, AbsenceDays, DeductionPerAbsent, Paid, PaymentDate);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int salaryID)
        {
            return clsMonthlySalaryData.Delete(salaryID);
        }

        public static clsMonthlySalary Find(int salaryID)
        {
            var dto = clsMonthlySalaryData.GetByID(salaryID);
            if (dto == null) return null;
            return new clsMonthlySalary(dto, enMode.Update);
        }

        public static List<MonthlySalaryDTO> GetAll()
        {
            return clsMonthlySalaryData.GetAll();
        }

        public static List<MonthlySalaryDTO> GetAllByEmployee(int employeeID)
        {
            return clsMonthlySalaryData.GetAllByEmployeeID(employeeID);
        }

        public static MonthlySalaryDTO GetByEmployeeAndMonth(int employeeID, DateTime salaryMonth)
        {
            return clsMonthlySalaryData.GetByEmployeeAndMonth(employeeID, salaryMonth);
        }

        public static (int TotalCount, decimal TotalNetSalary, decimal TotalPaid, decimal TotalUnpaid) GetSumsByMonth(DateTime month)
        {
            return clsMonthlySalaryData.GetSumsByMonth(month);
        }

        public bool UpdatePaidStatus(bool paid, DateTime? paymentDate)
        {
            bool success = clsMonthlySalaryData.UpdatePaidStatus(SalaryID, paid, paymentDate);
            if (success)
            {
                Paid = paid;
                PaymentDate = paymentDate;
            }
            return success;
        }
        public static MonthlySalaryDTO GetLastByEmployee(int employeeID)
        {
            return clsMonthlySalaryData.GetLastByEmployee(employeeID);
        }

        public static (int Count, decimal Total, decimal Paid, decimal Unpaid)
            GetSummaryByEmployee(int employeeID, DateTime start, DateTime end)
        {
            return clsMonthlySalaryData.GetSummaryByEmployeeAndPeriod(employeeID, start, end);
        }

        public static List<MonthlySalaryDTO> GetUnpaidByMonth(DateTime month)
        {
            return clsMonthlySalaryData.GetUnpaidByMonth(month);
        }

        public bool PartialUpdate(decimal? baseSalary = null, int? absenceDays = null,
            decimal? deductionPerAbsent = null, bool? paid = null, DateTime? paymentDate = null)
        {
            return clsMonthlySalaryData.PartialUpdate(SalaryID, baseSalary, absenceDays, deductionPerAbsent, paid, paymentDate);
        }

        public static decimal GetAverageNetSalaryByPeriod(DateTime start, DateTime end)
        {
            return clsMonthlySalaryData.GetAverageNetSalaryByPeriod(start, end);
        }

        public static List<(int Year, int Month)> GetMonthsWithUnpaid()
        {
            return clsMonthlySalaryData.GetMonthsWithUnpaid();
        }

        public static decimal GetTotalDeductionByEmployeePeriod(int employeeID, DateTime start, DateTime end)
        {
            return clsMonthlySalaryData.GetTotalDeductionByEmployeePeriod(employeeID, start, end);
        }

    }

    //public class clsMonthlySalary
    //{
    //    public int SalaryID { get; set; }
    //    public int EmployeeID { get; set; }
    //    public DateTime SalaryMonth { get; set; }
    //    public decimal BaseSalary { get; set; }
    //    public int AbsenceDays { get; set; }
    //    public decimal DeductionPerAbsent { get; set; }
    //    public decimal NetSalary => BaseSalary - (AbsenceDays * DeductionPerAbsent);
    //    public bool Paid { get; set; }
    //    public DateTime? PaymentDate { get; set; }

    //    public bool Save()
    //    {
    //        if (SalaryID == 0)
    //        {
    //            SalaryID = clsMonthlySalaryData.Insert(EmployeeID, SalaryMonth, BaseSalary, AbsenceDays, DeductionPerAbsent, Paid, PaymentDate);
    //            return SalaryID > 0;
    //        }
    //        return clsMonthlySalaryData.Update(SalaryID, EmployeeID, SalaryMonth, BaseSalary, AbsenceDays, DeductionPerAbsent, Paid, PaymentDate);
    //    }

    //    public static bool Delete(int salaryID)
    //    {
    //        return clsMonthlySalaryData.Delete(salaryID);
    //    }

    //    public static clsMonthlySalary Find(int salaryID)
    //    {
    //        var dto = clsMonthlySalaryData.GetByID(salaryID);
    //        if (dto == null) return null;

    //        return new clsMonthlySalary
    //        {
    //            SalaryID = dto.SalaryID,
    //            EmployeeID = dto.EmployeeID,
    //            SalaryMonth = dto.SalaryMonth,
    //            BaseSalary = dto.BaseSalary,
    //            AbsenceDays = dto.AbsenceDays,
    //            DeductionPerAbsent = dto.DeductionPerAbsent,
    //            Paid = dto.Paid,
    //            PaymentDate = dto.PaymentDate
    //        };
    //    }

    //    public static List<MonthlySalaryDTO> GetAll()
    //    {
    //        return clsMonthlySalaryData.GetAll();

    //    }

    //    public static List<MonthlySalaryDTO> GetByEmployeeAndMonth(int empID, DateTime month) => clsMonthlySalaryData.GetByEmployeeAndMonth(empID, month);

    //    public static (decimal Total, decimal Paid, decimal Unpaid) GetSums(DateTime month) => clsMonthlySalaryData.GetSalarySumsByMonth(month);
    //    public static List<MonthlySalaryDTO> GetAllByEmployeeID(int employeeID)
    //    {
    //        return clsMonthlySalaryData.GetAllByEmployeeID(employeeID);
    //    }
    //    public static bool SetPaidStatus(int salaryID, bool paid, DateTime? paymentDate = null)
    //    {
    //        return clsMonthlySalaryData.UpdatePaidStatus(salaryID, paid, paymentDate);
    //    }


    //}
}

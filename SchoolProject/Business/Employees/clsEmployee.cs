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
    public class clsEmployee
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int EmployeeID { get; set; }
        public int PersonID { get; set; }
        public int JobTitleID { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public decimal Salary { get; set; }

        public clsEmployee() { }

        public clsEmployee(EmployeeDTO dto, enMode mode = enMode.Update)
        {
            EmployeeID = dto.EmployeeID;
            PersonID = dto.PersonID;
            JobTitleID = dto.JobTitleID;
            HireDate = dto.HireDate;
            TerminationDate = dto.TerminationDate;
            IsActive = dto.IsActive;
            Notes = dto.Notes;
            Salary = dto.Salary;
            Mode = mode;
        }

        private bool _AddNew()
        {
            EmployeeID = clsEmployeeData.InsertEmployee(PersonID, JobTitleID, HireDate, TerminationDate, IsActive, Notes, Salary);
            return EmployeeID != -1;
        }

        private bool _Update()
        {
            return clsEmployeeData.UpdateEmployee(EmployeeID, PersonID, JobTitleID, HireDate, TerminationDate, IsActive, Notes, Salary);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _Update();
                default:
                    return false;
            }
        }

        public static bool Delete(int employeeID)
        {
            int rows = clsEmployeeData.DeleteEmployee(employeeID);
            return rows > 0;
        }

        public static clsEmployee Find(int employeeID)
        {
            var dto = clsEmployeeData.GetEmployeeByID(employeeID);
            if (dto == null) return null;
            return new clsEmployee(dto, enMode.Update);
        }

        public static clsEmployee FindByPhone(string Phone)
        {
            var dto = clsEmployeeData.GetEmployeeByPhone(Phone);
            if (dto == null) return null;
            return new clsEmployee(dto, enMode.Update);
        }
        public static clsEmployee FindByPersonId(int PersonID)
        {
            var dto = clsEmployeeData.GetEmployeeByPersonID(PersonID);
            if (dto == null) return null;
            return new clsEmployee(dto, enMode.Update);
        }
        public static List<EmployeeDTO> GetAll()
        {
            return clsEmployeeData.GetAllEmployees();
        }

        public static List<EmployeeDTO> GetActive()
        {
            return clsEmployeeData.GetActiveEmployees();
        }
        public static bool IsEmployeeExitByPersonID(int PersonID)
        {
            return clsEmployeeData.IsEmployeeExistByPersonID(PersonID);
        }
        public static bool IsEmployeeExitByPersonIDNotThis(int PersonID, int EmployeeID)
        {
            return clsEmployeeData.IsEmployeeExistByPersonIDAndNotThisEmployee(PersonID, EmployeeID);
        }

    }

}

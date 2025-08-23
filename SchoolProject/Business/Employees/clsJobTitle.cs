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
    public class clsJobTitle
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int JobTitleID { get; set; }
        public string JobTitleName { get; set; }
        public string Description { get; set; }
        public bool IsTeaching { get; set; }
        public bool IsAdministrative { get; set; }
        public bool CanTeach { get; set; }
        public bool RequiresCertification { get; set; }

        public clsJobTitle() { }

        public clsJobTitle(JobTitleDTO dto, enMode mode = enMode.Update)
        {
            JobTitleID = dto.JobTitleID;
            JobTitleName = dto.JobTitleName;
            Description = dto.Description;
            IsTeaching = dto.IsTeaching;
            IsAdministrative = dto.IsAdministrative;
            CanTeach = dto.CanTeach;
            RequiresCertification = dto.RequiresCertification;
            Mode = mode;
        }

        private bool _AddNew()
        {
            JobTitleID = clsJobTitleData.InsertJobTitle(
                JobTitleName, Description, IsTeaching, IsAdministrative, CanTeach, RequiresCertification
            );
            return JobTitleID != -1;
        }

        private bool _Update()
        {
            return clsJobTitleData.UpdateJobTitle(
                JobTitleID, JobTitleName, Description, IsTeaching, IsAdministrative, CanTeach, RequiresCertification
            );
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

        private static bool Delete(int jobTitleID)
        {
            int rowsAffected = clsJobTitleData.DeleteJobTitle(jobTitleID);
            return rowsAffected > 0;
        }

        public static clsJobTitle Find(int jobTitleID)
        {
            var dto = clsJobTitleData.GetJobTitleByID(jobTitleID);
            if (dto == null) return null;
            return new clsJobTitle(dto, enMode.Update);
        }

        public static List<JobTitleDTO> GetAll()
        {
            return clsJobTitleData.GetAllJobTitles();
        }
    }

}

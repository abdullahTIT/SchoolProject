using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Employees
{
    public class JobTitleDTO
    {
        public int JobTitleID { get; set; }
        public string JobTitleName { get; set; }
        public string Description { get; set; }
        public bool IsTeaching { get; set; }
        public bool IsAdministrative { get; set; }
        public bool CanTeach { get; set; }
        public bool RequiresCertification { get; set; }

        public JobTitleDTO() { }

        public JobTitleDTO(int jobTitleID, string jobTitleName, string description, bool isTeaching,
                           bool isAdministrative, bool canTeach, bool requiresCertification)
        {
            JobTitleID = jobTitleID;
            JobTitleName = jobTitleName;
            Description = description;
            IsTeaching = isTeaching;
            IsAdministrative = isAdministrative;
            CanTeach = canTeach;
            RequiresCertification = requiresCertification;
        }
    }

}

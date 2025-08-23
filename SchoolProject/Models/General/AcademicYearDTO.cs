using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.General
{
    public class AcademicYearDTO
    {
        public int AcademicYearID { get; set; }
        public string AcademicYearName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public AcademicYearDTO() { }

        public AcademicYearDTO(int academicYearID, string academicYearName, DateTime startDate, DateTime endDate)
        {
            AcademicYearID = academicYearID;
            AcademicYearName = academicYearName;
            StartDate = startDate;
            EndDate = endDate;
        }
    }

}

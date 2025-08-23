using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.General
{
    public class DayOfWeekDTO
    {
        public string DayName { get; set; }
        public bool IsWorkingDay { get; set; }

        public DayOfWeekDTO() { }

        public DayOfWeekDTO(string dayName, bool isWorkingDay)
        {
            DayName = dayName;
            IsWorkingDay = isWorkingDay;
        }
    }

}

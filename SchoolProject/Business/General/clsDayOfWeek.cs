using DataAccess.General;
using GlobalLayer;
using Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.General
{
    public class clsDayOfWeek
    {
        public string DayName { get; set; }
        public bool IsWorkingDay { get; set; }

        public clsDayOfWeek() { }

        public clsDayOfWeek(DayOfWeekDTO dto)
        {
            DayName = dto.DayName;
            IsWorkingDay = dto.IsWorkingDay;
        }


        private bool _Update()
        {
            return clsDaysOfWeekData.UpdateDay(DayName, IsWorkingDay);
        }


        public static clsDayOfWeek Find(string dayName)
        {
            var dto = clsDaysOfWeekData.GetDayByName(dayName);
            return dto == null ? null : new clsDayOfWeek(dto);
        }

        public static List<DayOfWeekDTO> GetAll() => clsDaysOfWeekData.GetAllDays();
        public static List<DayOfWeekDTO> GetActiveDays() => clsDaysOfWeekData.GetWorkingDays();
    }

}

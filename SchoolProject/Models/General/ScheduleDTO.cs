using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.General
{
    public class ScheduleDTO
    {
        public int ScheduleID { get; set; }
        public int ClassID { get; set; }
        public int SubjectID { get; set; }
        public int TeacherID { get; set; }
        public int RoomID { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int AcademicYearID { get; set; }
        public string Notes { get; set; }

        public ScheduleDTO() { }

        public ScheduleDTO(int scheduleID, int classID, int subjectID, int teacherID, int roomID, string dayOfWeek,
                           TimeSpan startTime, TimeSpan endTime, int academicYearID, string notes)
        {
            ScheduleID = scheduleID;
            ClassID = classID;
            SubjectID = subjectID;
            TeacherID = teacherID;
            RoomID = roomID;
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
            AcademicYearID = academicYearID;
            Notes = notes;
        }
    }

}

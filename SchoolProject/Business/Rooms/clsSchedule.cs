using DataAccess.General;
using DataAccess.Rooms;
using GlobalLayer;
using Models.General;
using Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rooms
{
    public class clsSchedule
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

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

        public clsSchedule() { }

        public clsSchedule(ScheduleDTO dto, enMode mode = enMode.Update)
        {
            ScheduleID = dto.ScheduleID;
            ClassID = dto.ClassID;
            SubjectID = dto.SubjectID;
            TeacherID = dto.TeacherID;
            RoomID = dto.RoomID;
            DayOfWeek = dto.DayOfWeek;
            StartTime = dto.StartTime;
            EndTime = dto.EndTime;
            AcademicYearID = dto.AcademicYearID;
            Notes = dto.Notes;
            Mode = mode;
        }

        private bool _AddNew()
        {
            ScheduleID = clsScheduleData.Insert(ClassID, SubjectID, TeacherID, RoomID, DayOfWeek, StartTime, EndTime, AcademicYearID, Notes);
            return ScheduleID != -1;
        }

        private bool _Update()
        {
            return clsScheduleData.Update(ScheduleID, ClassID, SubjectID, TeacherID, RoomID, DayOfWeek, StartTime, EndTime, AcademicYearID, Notes);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int scheduleID)
        {
            return clsScheduleData.Delete(scheduleID);
        }

        public static clsSchedule Find(int scheduleID)
        {
            var dto = clsScheduleData.GetByID(scheduleID);
            return dto == null ? null : new clsSchedule(dto);
        }

        public static List<ScheduleDTO> GetAll()
        {
            return clsScheduleData.GetAll();
        }

        public static List<ScheduleDTO> GetByClass(int classID)
        {
            return clsScheduleData.GetByClassID(classID);
        }

        public static List<ScheduleDTO> GetByTeacher(int teacherID)
        {
            return clsScheduleData.GetByTeacherID(teacherID);
        }

        public static List<ScheduleDTO> GetByAcademicYear(int academicYearID)
        {
            return clsScheduleData.GetByAcademicYearID(academicYearID);
        }
    }

}

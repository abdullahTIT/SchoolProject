using DataAccess.Rooms;
using GlobalLayer;
using Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rooms
{
    public class clsClass
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int StageID { get; set; }
        public int Capacity { get; set; }
        public string Notes { get; set; }

        public clsClass() { }

        public clsClass(ClassDTO dto, enMode mode = enMode.AddNew)
        {
            ClassID = dto.ClassID;
            ClassName = dto.ClassName;
            StageID = dto.StageID;
            Capacity = dto.Capacity;
            Notes = dto.Notes;
            Mode = mode;
        }

        private bool _AddNew()
        {
            ClassID = clsClassData.InsertClass(ClassName, StageID, Capacity, Notes);
            return ClassID != -1;
        }

        private bool _Update()
        {
            return clsClassData.UpdateClass(ClassID, ClassName, StageID, Capacity, Notes);
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

        public static bool Delete(int classID)
        {
            // يمكنك إضافة منطق تحقق هنا إذا أردت قبل الحذف (مثلاً وجود علاقات)
            return clsClassData.DeleteClass(classID);
        }

        public static clsClass Find(int classID)
        {
            var dto = clsClassData.GetClassByID(classID);
            if (dto == null) return null;
            return new clsClass(dto, enMode.Update);
        }

        public static List<ClassDTO> GetAll()
        {
            return clsClassData.GetAllClasses();
        }
    }

}

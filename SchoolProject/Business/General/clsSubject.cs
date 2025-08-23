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
    public class clsSubject
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int StageID { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }

        public clsSubject() { }

        public clsSubject(SubjectDTO dto, enMode mode = enMode.Update)
        {
            SubjectID = dto.SubjectID;
            SubjectName = dto.SubjectName;
            StageID = dto.StageID;
            IsActive = dto.IsActive;
            Notes = dto.Notes;
            Mode = mode;
        }

        private bool _AddNew()
        {
            SubjectID = clsSubjectData.Insert(SubjectName, StageID, IsActive, Notes);
            return SubjectID != -1;
        }

        private bool _Update()
        {
            return clsSubjectData.Update(SubjectID, SubjectName, StageID, IsActive, Notes);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int subjectID)
        {
            // هنا يمكن إضافة تحقق إذا كان الموضوع مرتبط بسجلات أخرى قبل الحذف
            return clsSubjectData.Delete(subjectID);
        }

        public static clsSubject Find(int subjectID)
        {
            var dto = clsSubjectData.GetByID(subjectID);
            return dto == null ? null : new clsSubject(dto);
        }

        public static List<SubjectDTO> GetAll()
        {
            return clsSubjectData.GetAll();
        }

        public static List<SubjectDTO> GetActive()
        {
            return clsSubjectData.GetActive();
        }

        public static List<SubjectDTO> GetByStage(int stageID)
        {
            return clsSubjectData.GetByStageID(stageID);
        }
    }

}

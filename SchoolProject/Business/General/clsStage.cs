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
    public class clsStage
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int StageID { get; set; }
        public string StageName { get; set; }
        public bool IsActive { get; set; }

        public clsStage() { }

        public clsStage(StageDTO dto, enMode mode = enMode.Update)
        {
            StageID = dto.StageID;
            StageName = dto.StageName;
            IsActive = dto.IsActive;
            Mode = mode;
        }

        private bool _AddNew()
        {
            StageID = clsStageData.InsertStage(StageName, IsActive);
            return StageID > 0;
        }

        private bool _Update()
        {
            return clsStageData.UpdateStage(StageID, StageName, IsActive);
        }

        public bool Save()
        {
            return Mode switch
            {
                enMode.AddNew => _AddNew(),
                enMode.Update => _Update(),
                _ => false
            };
        }

        public static bool Delete(int stageID) => clsStageData.DeleteStage(stageID);
        public static clsStage Find(int stageID)
        {
            var dto = clsStageData.GetStageByID(stageID);
            return dto == null ? null : new clsStage(dto);
        }

        public static List<StageDTO> GetAll() => clsStageData.GetAllStages();
        public static List<StageDTO> GetActive() => clsStageData.GetActiveStages();
    }

}

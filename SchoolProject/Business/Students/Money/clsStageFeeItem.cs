using DataAccess.Students;
using DataAccess.Students.Money;
using GlobalLayer;
using Models.Students.Money;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Students.Money
{
    public class clsStageFeeItem
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int StageFeeItemID { get; set; }
        public int StageID { get; set; }
        public int FeeTypeID { get; set; }
        public int AcademicYearID { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public clsStageFeeItem() { }

        public clsStageFeeItem(StageFeeItemDTO dto, enMode mode = enMode.Update)
        {
            StageFeeItemID = dto.StageFeeItemID;
            StageID = dto.StageID;
            FeeTypeID = dto.FeeTypeID;
            AcademicYearID = dto.AcademicYearID;
            Amount = dto.Amount;
            Description = dto.Description;
            IsActive = dto.IsActive;
            Mode = mode;
        }

        private bool _AddNew()
        {
            StageFeeItemID = clsStageFeeItemData.Insert(StageID, FeeTypeID, AcademicYearID, Amount, Description, IsActive);
            return StageFeeItemID != -1;
        }

        private bool _Update()
        {
            return clsStageFeeItemData.Update(StageFeeItemID, StageID, FeeTypeID, AcademicYearID, Amount, Description, IsActive);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int id)
        {
            return clsStageFeeItemData.Delete(id);
        }

        public static clsStageFeeItem Find(int id)
        {
            var dto = clsStageFeeItemData.GetByID(id);
            return dto == null ? null : new clsStageFeeItem(dto);
        }

        public static List<StageFeeItemDTO> GetAll()
        {
            return clsStageFeeItemData.GetAll();
        }

        public static List<StageFeeItemDTO> GetActive()
        {
            return clsStageFeeItemData.GetActive();
        }

        public bool UpdateIsActive(bool isActive)
        {
            IsActive = isActive;
            return clsStageFeeItemData.Update(StageFeeItemID, StageID, FeeTypeID, AcademicYearID, Amount, Description, isActive);
        }
    }

}

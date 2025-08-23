using DataAccess.Students;
using DataAccess.Students.Exams;
using DataAccess.Students.Money;
using GlobalLayer;
using Models.Students;
using Models.Students.Exams;
using Models.Students.Money;

namespace Business.Students.Money
{
    public class clsFeeType
    {
        public enum enMode { AddNew, Update }
        public enMode Mode = enMode.AddNew;

        public int FeeTypeID { get; set; }
        public string FeeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public clsFeeType() { }

        public clsFeeType(FeeTypeDTO dto, enMode mode = enMode.Update)
        {
            FeeTypeID = dto.FeeTypeID;
            FeeName = dto.FeeName;
            Description = dto.Description;
            IsActive = dto.IsActive;
            Mode = mode;
        }

        private bool _AddNew()
        {
            FeeTypeID = clsFeeTypeData.Insert(FeeName, Description, IsActive);
            return FeeTypeID != -1;
        }

        private bool _Update()
        {
            return clsFeeTypeData.Update(FeeTypeID, FeeName, Description, IsActive);
        }

        public bool Save()
        {
            return Mode == enMode.AddNew ? _AddNew() : _Update();
        }

        public static bool Delete(int feeTypeID)
        {
            return clsFeeTypeData.Delete(feeTypeID);
        }

        public static clsFeeType Find(int feeTypeID)
        {
            var dto = clsFeeTypeData.GetByID(feeTypeID);
            return dto == null ? null : new clsFeeType(dto);
        }

        public static List<FeeTypeDTO> GetAll()
        {
            return clsFeeTypeData.GetAll();
        }

        public static List<FeeTypeDTO> GetActive()
        {
            return clsFeeTypeData.GetActive();
        }

        public bool UpdateIsActive(bool isActive)
        {
            IsActive = isActive;
            return clsFeeTypeData.Update(FeeTypeID, FeeName, Description, isActive);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students.Money
{
    public class StageFeeItemDTO
    {
        public int StageFeeItemID { get; set; }
        public int StageID { get; set; }
        public int FeeTypeID { get; set; }
        public int AcademicYearID { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public StageFeeItemDTO() { }

        public StageFeeItemDTO(int id, int stageID, int feeTypeID, int yearID, decimal amount, string description, bool isActive)
        {
            StageFeeItemID = id;
            StageID = stageID;
            FeeTypeID = feeTypeID;
            AcademicYearID = yearID;
            Amount = amount;
            Description = description;
            IsActive = isActive;
        }
    }

}

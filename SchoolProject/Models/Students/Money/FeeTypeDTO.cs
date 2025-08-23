using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students.Money
{
    public class FeeTypeDTO
    {
        public int FeeTypeID { get; set; }
        public string FeeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public FeeTypeDTO() { }

        public FeeTypeDTO(int id, string name, string description, bool isActive)
        {
            FeeTypeID = id;
            FeeName = name;
            Description = description;
            IsActive = isActive;
        }
    }

}

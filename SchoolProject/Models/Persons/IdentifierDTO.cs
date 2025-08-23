using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Persons
{
    public class IdentifierDTO
    {
        public int IdentifierID { get; set; }
        public int IdentifierTypeID { get; set; }
        public string IdentifierValue { get; set; }

        public IdentifierDTO(int id, int typeId, string value)
        {
            IdentifierID = id;
            IdentifierTypeID = typeId;
            IdentifierValue = value;
        }
    }
}

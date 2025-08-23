using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Persons
{
    public class IdentifierTypeDTO
    {
        public IdentifierTypeDTO(int identifierTypeID, string identifierName)
        {
            IdentifierTypeID = identifierTypeID;
            IdentifierName = identifierName;
        }

        public int IdentifierTypeID { get; set; }
        public string IdentifierName { get; set; }
    }

}

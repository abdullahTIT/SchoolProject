using DataAccess.Persons;
using GlobalLayer;
using Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Persons
{
    public class IdentifierType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public IdentifierTypeDTO DTO
        {
            get { return new IdentifierTypeDTO(IdentifierTypeID, IdentifierName); }
        }

        public int IdentifierTypeID { get; set; }
        public string IdentifierName { get; set; }

        public IdentifierType(IdentifierTypeDTO dto, enMode mode = enMode.AddNew)
        {
            IdentifierTypeID = dto.IdentifierTypeID;
            IdentifierName = dto.IdentifierName;
            Mode = mode;
        }

        private bool _AddNew()
        {
            IdentifierTypeID = IdentifierTypeData.InsertIdentifierType(IdentifierName);
            return IdentifierTypeID != -1;
        }

        private bool _Update()
        {
            return IdentifierTypeData.UpdateIdentifierType(IdentifierTypeID, IdentifierName);
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
            }
            return false;
        }

        public static bool Delete(int identifierTypeID)
        {
            return IdentifierTypeData.DeleteIdentifierType(identifierTypeID);
        }

        public static IdentifierType Find(int identifierTypeID)
        {
            IdentifierTypeDTO dto = IdentifierTypeData.GetIdentifierTypeByID(identifierTypeID);
            if (dto != null)
                return new IdentifierType(dto, enMode.Update);
            else
                return null;
        }

        public static List<IdentifierTypeDTO> GetAll()
        {
            return IdentifierTypeData.GetAllIdentifierTypes();
        }
    }

}

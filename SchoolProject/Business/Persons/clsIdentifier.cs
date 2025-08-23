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
    public class clsIdentifier
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public IdentifierDTO DTO
        {
            get { return new IdentifierDTO(IdentifierID, IdentifierTypeID, IdentifierValue); }
        }

        public int IdentifierID { get; set; }
        public int IdentifierTypeID { get; set; }
        public string IdentifierValue { get; set; }

        public clsIdentifier(IdentifierDTO dto, enMode mode = enMode.AddNew)
        {
            IdentifierID = dto.IdentifierID;
            IdentifierTypeID = dto.IdentifierTypeID;
            IdentifierValue = dto.IdentifierValue;
            Mode = mode;
        }

        public clsIdentifier()
        {
            this.IdentifierTypeID = -1;
            this.IdentifierValue = string.Empty;
            this.IdentifierID = -1;
            this.Mode = enMode.AddNew;
        }
        private bool _AddNew()
        {
            // تحقق من وجود المعرف مسبقًا
            if (IdentifierData.DoesIdentifierExist(IdentifierValue))
            {
                Console.WriteLine("Error: Duplicate IdentifierValue.");
                return false;
            }

            IdentifierID = IdentifierData.InsertIdentifier(IdentifierTypeID, IdentifierValue);
            return IdentifierID != -1;
        }

        public bool DoesIdentifierExist(int ID)
        {
            return IdentifierData.DoesIdentifierExist(IdentifierValue);   
        }

        private bool _Update()
        {
            // تحقق من التكرار (مع استثناء المعرف الحالي)
            if (IdentifierData.DoesIdentifierExist(IdentifierValue, IdentifierID))
            {
                Console.WriteLine("Error: Duplicate IdentifierValue.");
                return false;
            }

            return IdentifierData.UpdateIdentifier(IdentifierID, IdentifierTypeID, IdentifierValue);
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

        public static bool Delete(int identifierID)
        {
            return IdentifierData.DeleteIdentifier(identifierID);
        }

        public static clsIdentifier Find(int identifierID)
        {
            IdentifierDTO dto = IdentifierData.GetIdentifierByID(identifierID);
            if (dto != null)
                return new clsIdentifier(dto, enMode.Update);
            else
                return null;
        }

        public static List<IdentifierDTO> GetAll()
        {
            return IdentifierData.GetAllIdentifiers();
        }
    }
}

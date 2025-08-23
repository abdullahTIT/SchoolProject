using DataAccess.Students;
using GlobalLayer;
using Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Students
{
    public class clsGuardian
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int GuardianID { get; set; }
        public int PersonID { get; set; }
        public string Jobs { get; set; }
        public string Notes { get; set; }

        public clsGuardian() { }

        public clsGuardian(GuardianDTO dto, enMode mode = enMode.Update)
        {
            GuardianID = dto.GuardianID;
            PersonID = dto.PersonID;
            Jobs = dto.Jobs;
            Notes = dto.Notes;
            Mode = mode;
        }

        private bool _AddNew()
        {
            if (clsGuardianData.DoesPersonHaveGuardian(PersonID))
            {
                Console.WriteLine("Guardian already exists for this PersonID.");
                return false;
            }
            if (clsGuardianData.IsPersonLinkedToGuardian(PersonID))
            {
                return false;
            }

            GuardianID = clsGuardianData.InsertGuardian(PersonID, Jobs, Notes);
            return GuardianID != -1;
        }

        private bool _Update()
        {
            if (!clsGuardianData.DoesPersonHaveGuardian(GuardianID))
            {
                Console.WriteLine("Guardian does not exist.");
                return false;
            }
            if (clsGuardianData.IsPersonLinkedToGuardian(PersonID))
            {
                return false;
            }

            return clsGuardianData.UpdateGuardian(GuardianID, PersonID, Jobs, Notes);
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
                default:
                    return false;
            }
        }

        public static bool Delete(int guardianID)
        {
            return clsGuardianData.DeleteGuardian(guardianID);
        }

        public static clsGuardian Find(int guardianID)
        {
            var dto = clsGuardianData.GetGuardianByID(guardianID);
            if (dto == null) return null;
            return new clsGuardian(dto, enMode.Update);
        }

        public static List<GuardianDTO> GetAll()
        {
            return clsGuardianData.GetAllGuardians();
        }

        public static List<GuardianDTO> GetByPersonID(int personID)
        {
            return clsGuardianData.GetGuardiansByPersonID(personID);
        }

        public static bool Exists(int personID)
        {
            return clsGuardianData.DoesPersonHaveGuardian(personID);
        }
    }

}

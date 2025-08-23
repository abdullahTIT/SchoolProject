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
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public int? IdentifierID { get; set; }
        public bool Gender { get; set; }
        public DateTime? BirthDate { get; set; }

        public string? Phone { get; set; }     
        public string? Email { get; set; }     
        public string? Address { get; set; }   
        public string? ImagePath { get; set; }

        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.IdentifierID = null;
            this.Gender = true;
            this.BirthDate = DateTime.Now.AddYears(-5);
            this.Phone = null;
            this.Email = null;
            this.Address = null;
            this.ImagePath = null;
            this.Mode = enMode.AddNew;
        }

        public clsPerson(PersonDTO dto, enMode mode = enMode.AddNew)
        {
            PersonID = dto.PersonID;
            FirstName = dto.FirstName;
            SecondName = dto.SecondName;
            ThirdName = dto.ThirdName;
            LastName = dto.LastName;
            IdentifierID = dto.IdentifierID;
            Gender = dto.Gender;
            BirthDate = dto.BirthDate;
            Phone = dto.Phone;
            Email = dto.Email;
            Address = dto.Address;
            ImagePath = dto.ImagePath;
            Mode = mode;
        }

        private bool _AddNew()
        {
            if (string.IsNullOrEmpty(Phone))
            {
                return false;

            }
            if (clsPersonData.DoesPhoneExist(Phone))
            {
                Console.WriteLine("Error: Phone number already exists.");
                return false;
            }
            if (!string.IsNullOrEmpty(Email))
            {
                if (clsPersonData.DoesEmailExist(Email))
                {
                    Console.WriteLine("Error: Email already exists.");
                    return false;
                }
            }
            PersonID = clsPersonData.InsertPerson(
                FirstName, SecondName, ThirdName, LastName, IdentifierID,
                Gender, BirthDate, Phone, Email, Address, ImagePath
            );
            return PersonID != -1;
        }
        public static bool DoesPhoneExist(string phone, int? excludePersonID = null)
        {
            return clsPersonData.DoesPhoneExist(phone, excludePersonID);
        }
        public static bool doesEmailExist(string email, int? excludePersonID = null)
        {
            return clsPersonData.DoesEmailExist(email, excludePersonID);
        }
        private bool _Update()
        {
            if (string.IsNullOrEmpty(Phone))
            {
                return false;

            }
            if (clsPersonData.DoesPhoneExist(Phone))
            {
                Console.WriteLine("Error: Phone number already exists.");
                return false;
            }
            if (!string.IsNullOrEmpty(Email))
            {
                if (clsPersonData.DoesEmailExist(Email))
                {
                    Console.WriteLine("Error: Email already exists.");
                    return false;
                }
            }
            return clsPersonData.UpdatePerson(
                PersonID, FirstName, SecondName, ThirdName, LastName, IdentifierID,
                Gender, BirthDate, Phone, Email, Address, ImagePath
            );
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

        public static bool Delete(int personID)
        {
            if (clsPersonData.IsPersonRelated(personID))
            {
                return false;
            }
            return clsPersonData.DeletePerson(personID);
        }

        public static clsPerson Find(int personID)
        {
            var dto = clsPersonData.GetPersonByID(personID);
            if (dto == null) return null;
            return new clsPerson(dto, enMode.Update);
        }

        public static clsPerson Find(string phone)
        {
            var dto = clsPersonData.GetPersonByPhone(phone);
            if (dto == null) return null;
            return new clsPerson(dto, enMode.Update);
        }

        public static List<PersonDTO> GetAll()
        {
            return clsPersonData.GetAllPersons();
        }

        public static List<PersonDTO> GetPersonsPaged(int offset = 0, int fetch = 50, string search = null)
        {
            return clsPersonData.GetPersonsPaged(offset, fetch, search);
        }
    }

}

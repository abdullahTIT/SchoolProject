using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Persons
{
    public class PersonDTO
    {
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

        public PersonDTO() { }

        public PersonDTO(int personID, string firstName, string secondName, string thirdName, string lastName, int? identifierID,
                     bool gender, DateTime? birthDate, string? phone, string? email, string? address, string? imagePath)
        {
            PersonID = personID;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            IdentifierID = identifierID;
            Gender = gender;
            BirthDate = birthDate;
            Phone = phone;
            Email = email;
            Address = address;
            ImagePath = imagePath;
        }
    }

}

using DataAccess.Emloyees;
using Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Employees
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsUser() { }

        public clsUser(UserDTO dto, enMode mode = enMode.AddNew)
        {
            UserID = dto.UserID;
            PersonID = dto.PersonID;
            UserName = dto.UserName;
            Password = dto.Password;
            IsActive = dto.IsActive;
            Mode = mode;
        }

        private bool _AddNew()
        {
            if (clsUserData.DoesUserNameExist(UserName))
            {
                
                return false;
            }

            UserID = clsUserData.InsertUser(PersonID, UserName, Password, IsActive);
            return UserID != -1;
        }

        private bool _Update()
        {
            if (clsUserData.DoesUserNameExist(UserName, UserID))
            {
                return false;
            }

            return clsUserData.UpdateUser(UserID, PersonID, UserName, Password, IsActive);
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

        public static bool Delete(int userID)
        {
            return clsUserData.DeleteUser(userID);
        }

        public static clsUser Find(int userID)
        {
            var dto = clsUserData.GetUserByID(userID);
            if (dto == null) return null;
            return new clsUser(dto, enMode.Update);
        }

        public static clsUser Find(string userName, string password)
        {
            var dto = clsUserData.GetUserByUserNameAndPassword(userName, password);
            if (dto == null) return null;
            return new clsUser(dto, enMode.Update);
        }

        public static List<UserDTO> GetAll()
        {
            return clsUserData.GetAllUsers();
        }
        

    }

}

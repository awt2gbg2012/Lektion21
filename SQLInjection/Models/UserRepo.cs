using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLInjection.Models
{
    public class UserRepo
    {
        private static UserRepo _instance;
        public static UserRepo Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new UserRepo();
                return _instance;
            }
        }
        private string _password;
        private UserRepo()
        {
            _password = "goodOldPassword";
        }

        public string GetPasswordForUser(Guid UserID) { return _password; }
        public void SetPasswordForUser(Guid UserID, string newPassword) { _password = newPassword; }
    }
}
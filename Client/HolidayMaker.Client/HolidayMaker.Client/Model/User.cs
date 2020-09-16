using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMaker.Client.Model
{
    class User
    {
        private int UserId { get; set; }
        private string Email { get; set; }
        private string Password { get; set; }
        private string ConfirmPassword { get; set; }

        public User(string email, string password, string confirmPassword )
        {
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}

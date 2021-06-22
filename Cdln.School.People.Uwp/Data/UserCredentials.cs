using System;

namespace Cdln.School.People.Uwp.Data
{
    public struct UserCredentials
    {
        public string Email { get; }
        public string Password { get; }
        public UserCredentials(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}

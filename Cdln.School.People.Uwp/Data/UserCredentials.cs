using System;

namespace Cdln.School.People.Uwp
{
    public class UserCredentials : IUserCredentials
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Password { get; }
        public UserCredentials(Guid id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }
    }
}

using System;

namespace Cdln.School.People.Uwp
{
    public interface IUserCredentials
    {
        Guid Id { get; }
        string Email { get; }
        string Password { get; }
    }
}

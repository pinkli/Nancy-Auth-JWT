using System;
using Nancy.Security;

namespace Molaware.Nancy.Auth.JWT
{
    interface ITokenValidator
    {
        IUserIdentity ValidateUser(string token);
    }
}


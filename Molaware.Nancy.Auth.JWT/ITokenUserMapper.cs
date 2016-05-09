using System;
using Nancy.Security;

namespace Molaware.Nancy.Auth.JWT
{
    public interface ITokenUserMapper
    {
        IUserIdentity Convert(JwtToken token);
    }
}


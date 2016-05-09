using System;
using Nancy.Security;

namespace Molaware.Nancy.Auth.JWT
{
    class DefaultTokenUserMapper : ITokenUserMapper
    {
        #region ITokenUserMapper implementation
        public IUserIdentity Convert(JwtToken token)
        {
            return new DefaultUserIdentity {
                UserName = token.UserName,
                Claims = token.Claims
            };
        }
        #endregion
        
    }
}


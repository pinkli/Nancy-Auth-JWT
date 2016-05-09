using System;
using Molaware.Nancy.Auth.JWT;

namespace TestSelfhostJwt
{
    public class AuthOptionsProvider : IAuthOptionsProvider
    {
        // add a file like this to Enable the JWT Auth thing.
        // if not present, auth will not take effect

        public AuthOptions Configure()
        {
            return new AuthOptions {
                IgnorePaths = new[] { "/login", "/info", "/" },
                WWWAuthenticationChallenge = "/login"
            };
        }


    }
}


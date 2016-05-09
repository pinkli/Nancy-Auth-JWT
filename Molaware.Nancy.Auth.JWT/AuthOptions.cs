using System;
using System.Collections.Generic;

namespace Molaware.Nancy.Auth.JWT
{
    public class AuthOptions
    {
        public IEnumerable<string> IgnorePaths { get; set; }
        public string WWWAuthenticationChallenge { get; set; }
        public bool PassThruUnAuthorizedRequests { get; set; }
    }
}


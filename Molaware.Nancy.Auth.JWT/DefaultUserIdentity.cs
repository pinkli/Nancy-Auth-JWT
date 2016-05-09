using System;
using System.Collections.Generic;
using Nancy.Security;

namespace Molaware.Nancy.Auth.JWT
{
    public class DefaultUserIdentity : IUserIdentity
    {
        public string UserName { get; set; }

        public IEnumerable<string> Claims { get; set; }
    }
}


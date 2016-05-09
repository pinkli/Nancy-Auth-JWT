using System;
using System.Collections.Generic;
using System.Linq;

namespace Molaware.Nancy.Auth.JWT
{
    public class JwtToken
    {
        public string UserName { get; set; }

        public DateTime Expire { get; set; }

        public IEnumerable<string> Claims { get; set; }
    }
}


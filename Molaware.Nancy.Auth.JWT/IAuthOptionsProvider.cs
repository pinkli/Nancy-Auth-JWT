using System;

namespace Molaware.Nancy.Auth.JWT
{
    public interface IAuthOptionsProvider
    {
        AuthOptions Configure();
    }
}


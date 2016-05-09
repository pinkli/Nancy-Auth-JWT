using System;
using JWT;

namespace Molaware.Nancy.Auth.JWT
{
    class JwtTokenEncoder
    {
        private readonly ISecurekeyProvider key;

        public JwtTokenEncoder(ISecurekeyProvider key)
        {
            this.key = key;
        }

        public string Encode(JwtToken token)
        {
            return JsonWebToken.Encode(token, this.key.GetSecurekey(), JwtHashAlgorithm.HS256);
        }
    }
}


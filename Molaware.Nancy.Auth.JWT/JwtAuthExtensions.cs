using System;
using Nancy;
using Nancy.TinyIoc;

namespace Molaware.Nancy.Auth.JWT
{
    public static class JwtAuthExtensions
    {
        public static string JwtToken(this NancyModule module, JwtToken token, ISecurekeyProvider keyProvider)
        {
            #if DEBUG
            Console.WriteLine("request: " + keyProvider.GetType().FullName);
            #endif

            var encoder = new JwtTokenEncoder(keyProvider);
            string encoded = encoder.Encode(token);
            return encoded;
        }
    }
}


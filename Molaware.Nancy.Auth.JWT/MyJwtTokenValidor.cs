using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Nancy.Security;
using JWT;

namespace Molaware.Nancy.Auth.JWT
{
    sealed class MyJwtTokenValidor : ITokenValidator
    {

        private readonly ISecurekeyProvider secret;
        private readonly ITokenUserMapper mapper;

        public MyJwtTokenValidor(ISecurekeyProvider securekeyProvider, ITokenUserMapper userMapper)
        {
            this.secret = securekeyProvider;
            this.mapper = userMapper;
        }


        #region ITokenValidator implementation

        public IUserIdentity ValidateUser(string token)
        {
            try
            {
                string securekey = this.secret.GetSecurekey();

                #if DEBUG
                Console.WriteLine(securekey);
                #endif

                var decoded = JsonWebToken.DecodeToObject(token, securekey) as Dictionary<string, object>; 

                JwtToken tk = new JwtToken 
                {
                    UserName = decoded["UserName"].ToString(),
                    Expire = DateTime.Parse(decoded["Expire"].ToString())
                };

                if (decoded.ContainsKey("Claims"))
                {
                    var claims = new List<string>();
                    var decodedClaims = (ArrayList)decoded["Claims"];
                    for (int i = 0; i < decodedClaims.Count; i++)
                    {
                        string claim = decodedClaims[i].ToString();
                        claims.Add(claim);
                    }

                    tk.Claims = claims;
                }

                if (tk.Expire < DateTime.UtcNow)  // expires
                    return null;

                return this.mapper.Convert(tk);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}


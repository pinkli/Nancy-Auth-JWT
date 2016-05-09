using System;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

using Molaware.Nancy.Auth.JWT;

namespace TryOwinSelfHost
{
    public class HelloModule : NancyModule
    {
        public HelloModule(ISecurekeyProvider keyProvider)
        {
            StaticConfiguration.DisableErrorTraces = false;

            Get["/"] = x => "Hello World from Owin SelfHosting";

            Get["/api"] = x => 
            {
                
                var data = new[] {
                    new Data { Id = 1, Name = "one", Date = DateTime.Today },
                    new Data { Id = 2, Name = "two", Date = DateTime.Now },
                };

                return data;
            };

            Get["/safeapi"] = x => 
            {
                this.RequiresClaims(new[] { "gender:woman" });  // more checks than default

                var data = new[] {
                    new Data { Id = 1, Name = "one", Date = DateTime.Today },
                    new Data { Id = 2, Name = "two", Date = DateTime.Now },
                };

                return data;
            };


            Post["/login"] = x =>
            {
                var info = this.Bind<LoginInfo>();

                if (info.UserName == "cate")  // ok
                {
                    var jwt = new JwtToken {
                        UserName = info.UserName,
                        Expire = DateTime.UtcNow.AddDays(2),
                        Claims = new[] { "age:20", "gender:woman" }
                    };

                    return this.JwtToken(jwt, keyProvider);
                }
                else if (info.UserName == "bob")
                {
                    var jwt = new JwtToken {
                        UserName = info.UserName,
                        Expire = DateTime.UtcNow.AddDays(2),
                        Claims = new[] { "age:20", "gender:man" }
                    };

                    return this.JwtToken(jwt, keyProvider);
                }

                return HttpStatusCode.Unauthorized;
            };
        }
    }

    class LoginInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Data  // need to be public when auto content-negotiation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}


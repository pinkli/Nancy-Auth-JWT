using System;
using System.Linq;

using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

using Molaware.Nancy.Auth.JWT;

namespace TestSelfhostJwt
{
    public class HelloModule : NancyModule
    {
        
        public HelloModule(ISecurekeyProvider securekey)  // passing this dependency
        {
            StaticConfiguration.DisableErrorTraces = false;

            Get["/"] = x => "Hello World from SelfHosting with JWT auth";  // should be ignored hereby auth option


            Get["/safeapi"] = x => 
            {
                this.RequiresClaims(new[] { "gender:man" });  // more checks than default

                var data = new[] {
                    new Data { Id = 1, Name = "one", Date = DateTime.Today },
                    new Data { Id = 2, Name = "two", Date = DateTime.Now },
                };

                return data;
            };

            Get["/api"] = x => 
            {
                
                var data = new[] {
                    new Data { Id = 1, Name = "one", Date = DateTime.Today },
                    new Data { Id = 2, Name = "two", Date = DateTime.Now },
                };

                return data;
            };


            Post["/login"] = x =>  // should be ignored hereby auth option
            {
                var info = this.Bind<LoginInfo>();

                if (info.UserName == "cate")  // ok
                {
                    var jwt = new JwtToken {
                        UserName = info.UserName,
                        Expire = DateTime.UtcNow.AddDays(2),
                        Claims = new[] { "age:20", "gender:woman" }
                    };

                    return this.JwtToken(jwt, securekey);
                }
                else if (info.UserName == "bob")
                {
                    var jwt = new JwtToken {
                        UserName = info.UserName,
                        Expire = DateTime.UtcNow.AddDays(2),
                        Claims = new[] { "age:20", "gender:man" }
                    };

                    return this.JwtToken(jwt, securekey);
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


using System;
using Owin;
using Molaware.Nancy.Auth.JWT;

namespace TryOwinSelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {


            app.UseNancy();

        }


    }
}


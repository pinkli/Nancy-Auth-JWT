using System;
using Nancy.Conventions;

namespace TryNancyNginx
{
    public class MyBootstrapper : Nancy.DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            //base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.AddDirectory("doc", "swagger-ui");
        }
    }
}


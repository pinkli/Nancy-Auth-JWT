using System;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Molaware.Nancy.Auth.JWT
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);


            if (!container.CanResolve<IAuthOptionsProvider>())
                return;  // if no implementation detected, no actions will be made

            this.RegisterDependencies(container);



            var auth = container.Resolve<StatelessAuth>();

            auth.Enable(pipelines);
        }

        private void RegisterDependencies(TinyIoCContainer container)
        {
            
            #if DEBUG
                var securekeyProvider = container.Resolve<ISecurekeyProvider>();
                Console.WriteLine("application: " + securekeyProvider.GetType().FullName);
            #endif



            container.Register<ITokenValidator, MyJwtTokenValidor>();  // always use this one
           
        }
    }
}


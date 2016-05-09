using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Molaware.Nancy.Auth.JWT
{
    public class CheckAuthOnStartup : IApplicationStartup
    {
        private readonly StatelessAuth auth;

        public CheckAuthOnStartup(AuthOptions options)
        {
            var validator = TinyIoCContainer.Current.Resolve<ITokenValidator>();
            this.auth = new StatelessAuth(validator, options);
        }

        #region IApplicationStartup implementation

        public void Initialize(IPipelines pipelines)
        {
            this.auth.Enable(pipelines);
        }

        #endregion
    }
}


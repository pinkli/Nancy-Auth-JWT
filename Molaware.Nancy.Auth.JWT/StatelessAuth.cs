using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

using Nancy;
using Nancy.Bootstrapper;

namespace Molaware.Nancy.Auth.JWT
{
    class StatelessAuth
    {
        private readonly ITokenValidator validator;
        private readonly IAuthOptionsProvider optionsProvider;
        private readonly AuthOptions options;

        private const string TokenName = "Authorization";

        public StatelessAuth(ITokenValidator tokenValidator, IAuthOptionsProvider optionsProvider)
        {
            this.validator = tokenValidator;
            this.optionsProvider = optionsProvider;

            this.options = this.optionsProvider.Configure();
        }

        public void Enable(IPipelines pipelines)
        {
            pipelines.BeforeRequest.AddItemToStartOfPipeline(CheckToken);
        }

        private Response CheckToken(NancyContext context)
        {
            var path = Uri.UnescapeDataString(context.Request.Path);

            // check paths to ignore auth checking
            if (this.options != null && this.options.IgnorePaths != null)
            {
                foreach (var ignore in this.options.IgnorePaths)
                {
                    var matcher = new Minimatch.Minimatcher(ignore, new Minimatch.Options { IgnoreCase = true });

                    if (matcher.IsMatch(path))
                    {
                        return null;
                    }
                }
            }

            // check auth header token
            string token = context.Request.Headers.Authorization;
            if (string.IsNullOrWhiteSpace(token))
                return AuthChallengeResponse(context);

            var user = this.validator.ValidateUser(token);

            if (user == null)
                return AuthChallengeResponse(context);

            // token is valid, set it to CurrentUser
            context.CurrentUser = user;

            return null;
        }

        private Response AuthChallengeResponse(NancyContext context)
        {
            if (this.options != null && this.options.PassThruUnAuthorizedRequests)  // just pass thru
                return null;  // do nothing

            var resp = new Response();
            resp.StatusCode = HttpStatusCode.Unauthorized;

            // set www-authentication header
            if (this.options != null && !string.IsNullOrWhiteSpace(this.options.WWWAuthenticationChallenge))
            {
                string challengePath = this.options.WWWAuthenticationChallenge;

                resp.WithHeader("WWW-Authenticate", challengePath);

            }

            return resp;

        }
    }
}


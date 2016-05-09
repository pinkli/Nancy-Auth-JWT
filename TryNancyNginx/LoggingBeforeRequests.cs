using System;
using Nancy;
using Nancy.Bootstrapper;

namespace TryNancyNginx
{
    public class LoggingBeforeRequests : IRequestStartup
    {
        

        public LoggingBeforeRequests()
        {
        }

        #region IRequestStartup implementation

        public void Initialize(IPipelines pipelines, NancyContext context)
        {
            pipelines.BeforeRequest.AddItemToEndOfPipeline(ctx => {
                
                Console.WriteLine(ctx.Request.Method + " " + ctx.Request.Url.ToString());  // log to console

                return ctx.Response;
            });

            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) =>
            {
                return HttpStatusCode.BadRequest;
            });
        }

        #endregion
    }
}


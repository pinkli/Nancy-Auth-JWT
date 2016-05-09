using System;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Swagger;

namespace TryNancyNginx
{
    public class HelloModule : NancyModule
    {
        
        public HelloModule()
        {
            StaticConfiguration.DisableErrorTraces = false;

            Get["index", "/"] = x => "Hello World";

            Get["entry", "/api"] = x =>
            {
                var data = new[] {
                    new Data { Id = 1, Name = "one", Date = DateTime.Today },
                    new Data { Id = 2, Name = "two", Date = DateTime.Now },
                };

                return data;
            };


            Get["single", "/api/{id}"] = x =>
            {
                string _id = x.id;

                int id;
                if (!int.TryParse(_id, out id))
                    return 400; // bad request

                return new Data{ Id = id, Name = "one", Date = DateTime.Now};
            };

            Post["post", "/api"] = x =>
            {
                Data posted = this.Bind<Data>("date");
                // more specific handling or model error;
                // pipelines.OnError handle this in a general way
//                try
//                {
//                    posted = this.Bind<Data>("date");
//                }
//                catch
//                {
//                    return HttpStatusCode.BadRequest;
//                }

                System.Diagnostics.Debug.Assert(posted != null);

                posted.Date = DateTime.Today.AddYears(1);
                return posted;
            };

        }
    }


    // naming is essential here: {name}MetadataModule
    public class HelloMetadataModule : Nancy.Metadata.Modules.MetadataModule<Nancy.Swagger.SwaggerRouteData>
    {
        public HelloMetadataModule()
        {
            Describe["entry"] = description => description.AsSwagger(with =>
            {
                with.ResourcePath("/api");
                with.Summary("The list of data");
                with.Notes("This returns a list of data from our awesome app");
                with.Model<Data>();
            });
                
        }
    }


    public class Data  // need to be public when auto content-negotiation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }


}


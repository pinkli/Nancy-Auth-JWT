using System;
using Nancy.Hosting.Self;

namespace TestSelfhostJwt
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string uri = "http://localhost:8899";

            Console.WriteLine($"starting Nancy on {uri}");

            // new  a nancy host
            var conf = new HostConfiguration() {
                RewriteLocalhost = false
            };

            var host = new NancyHost(conf, new Uri(uri));
            //var host = new NancyHost(new Uri(uri));
            host.Start();

            Console.ReadLine();

            Console.WriteLine("stopping Nancy");
            host.Stop();
        }
    }
}

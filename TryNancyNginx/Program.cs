using System;
using Nancy.Hosting.Self;
using Mono.Unix;
using Mono.Unix.Native;

namespace TryNancyNginx
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //string uri = "http://127.0.0.1:8888";  // this works without the conf
            string uri = "http://localhost:8888";

            Console.WriteLine($"starting Nancy on {uri}");

            // new  a nancy host
            var conf = new HostConfiguration() {
                RewriteLocalhost = false
            };

            var host = new NancyHost(conf, new Uri(uri));
            //var host = new NancyHost(new Uri(uri));
            host.Start();

            // if running on Mono
            if (Type.GetType("Mono.Runtime") != null)
            {
                UnixSignal.WaitAny(new [] {
                    new UnixSignal(Signum.SIGINT),
                    new UnixSignal(Signum.SIGTERM),
                    new UnixSignal(Signum.SIGQUIT),
                    new UnixSignal(Signum.SIGHUP),
                });
            }
            else
            {
                Console.ReadLine();
            }

            Console.WriteLine("stopping Nancy");
            host.Stop();
        }
    }
}

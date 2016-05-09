using System;
using Microsoft.Owin.Hosting;

using static System.Console;

namespace TryOwinSelfHost
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string url = "http://localhost:8889";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Running on {url}");
                Console.WriteLine("Press enter to exit");

                ReadLine();
            }
        }
    }
}

using System;
using System.Net;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Nancy;
using NancyController.Poc;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace NancyController.Poc
{
    /// <summary>
    /// Startupclass Required for OWIN hosting
    /// </summary>
    public class Startup
    {
        //http://patrickdesjardins.com/blog/owin-platform-with-nancy-and-signalr
        /// <summary>
        /// The Configuration Entry
        /// </summary>
        /// <param name="app">The AppBuilder passed along by OWIN</param>
        public void Configuration(IAppBuilder app)
        {
            // Disable Authentication
            var listener = (HttpListener)app.Properties["System.Net.HttpListener"];
            listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            // Tel the app to use Nancy
            app.UseNancy(options =>
            {
                options.PerformPassThrough = context => false;
                options.Bootstrapper = new NancyControllerBootStrapper();
            });
        }
    }

    class Program
    {
        [STAThread]
        public static void Main()
        {
            using (WebApp.Start<Startup>("http://+:8080"))
            {
                Console.WriteLine("Running on: http://localhost:8080");
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }
    }
}
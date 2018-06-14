using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
//using Microsoft.Owin.Cors;


[assembly: OwinStartup(typeof(WebApplication2.Startup))]

namespace WebApplication2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(30);
            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(110);
            GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(10);


            GlobalHost.Configuration.DefaultMessageBufferSize = 500;
            //GlobalHost.DependencyResolver.UseRedis("localhost", 6379, "pwd", "Recipe39");
            //string connectionString = "[connection string]";
            //GlobalHost.DependencyResolver.UseSqlServerSqlServer(connectionString);

            app.MapSignalR();
            app.MapSignalR<HubPersistent>("/persistent");


            //app.Map("/signalr", map =>
            //{
            //    map.UseCors(CorsOptions.AllowAll);
            //    map.RunSignalR();
            //});
        }
    }
}

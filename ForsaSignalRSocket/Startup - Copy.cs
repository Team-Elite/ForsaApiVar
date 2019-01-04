using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ForsaSignalRSocket.Startup))]
namespace ForsaSignalRSocket
{

    public class Startup
    {
       // public Microsoft.Owin.Cors.CorsOptions AllowAll { get;  set; }

        public void Configuration(IAppBuilder app)
        {
           
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.MapSignalR();
            // For more information on how to config()ure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}

using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ForsaSignalRSocket.Startup))]

namespace ForsaSignalRSocket
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          
            app.MapSignalR();
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}

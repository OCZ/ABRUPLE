using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Abruple.App.Startup))]
namespace Abruple.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}

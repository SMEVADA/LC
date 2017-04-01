using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LetsConnect.Startup))]
namespace LetsConnect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

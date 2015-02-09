using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WinAtWaiting.Startup))]
namespace WinAtWaiting
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DMCombatScreen.WebMVC.Startup))]
namespace DMCombatScreen.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

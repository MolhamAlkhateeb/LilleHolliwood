using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LilleHaboov.Startup))]
namespace LilleHaboov
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

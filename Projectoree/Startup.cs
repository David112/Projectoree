using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projectoree.Startup))]
namespace Projectoree
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

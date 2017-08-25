using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OMSIFYP.Startup))]
namespace OMSIFYP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

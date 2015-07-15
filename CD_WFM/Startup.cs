using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WFM.Startup))]
namespace WFM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

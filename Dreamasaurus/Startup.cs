using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dreamasaurus.Startup))]
namespace Dreamasaurus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

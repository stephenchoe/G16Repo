using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(G16MG.Startup))]
namespace G16MG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
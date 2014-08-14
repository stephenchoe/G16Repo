using Microsoft.Owin;
using Owin;

//[assembly: OwinStartupAttribute(typeof(WebApplicationG16_2013.Startup))]
[assembly: OwinStartup(typeof(WebApplicationG16_2013.Startup))]
namespace WebApplicationG16_2013
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

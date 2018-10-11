using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Odigo.Web.Startup))]
namespace Odigo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

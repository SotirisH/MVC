using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aurora.SMS.Web.Startup))]
namespace Aurora.SMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

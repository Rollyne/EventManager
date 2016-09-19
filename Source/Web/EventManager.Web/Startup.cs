using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(EventManager.Web.Startup))]

namespace EventManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}

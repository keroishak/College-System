using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(College_System.Startup))]
namespace College_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

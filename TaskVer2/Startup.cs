using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskVer2.Startup))]
namespace TaskVer2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

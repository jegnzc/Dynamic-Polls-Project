using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevelTest.Startup))]
namespace DevelTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

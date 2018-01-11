using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SehatClone.Startup))]
namespace SehatClone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

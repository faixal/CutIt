using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CutIt.Startup))]
namespace CutIt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

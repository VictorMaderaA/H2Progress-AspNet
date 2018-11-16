using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hack2ProgressAspNet.Startup))]
namespace Hack2ProgressAspNet
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

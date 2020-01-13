using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Registros.Startup))]
namespace Registros
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MunicipalTrashProgram.Startup))]
namespace MunicipalTrashProgram
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

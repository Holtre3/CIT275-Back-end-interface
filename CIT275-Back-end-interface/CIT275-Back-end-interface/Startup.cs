using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CIT275_Back_end_interface.Startup))]
namespace CIT275_Back_end_interface
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

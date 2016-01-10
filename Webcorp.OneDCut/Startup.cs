using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Webcorp.OneDCut.Startup))]
namespace Webcorp.OneDCut
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }



    }
}

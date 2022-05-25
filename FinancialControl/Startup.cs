using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinancialControl.Startup))]
namespace FinancialControl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

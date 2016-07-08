using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Connect4.Startup))]

namespace Connect4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}

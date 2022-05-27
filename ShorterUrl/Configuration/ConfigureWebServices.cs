using ShortenerUrl.Web.Interfaces;
using ShortenerUrl.Web.Services;

namespace ShortenerUrl.Web.Configuration
{
    public class ConfigureWebServices
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ILinkVmService, LinkVmService>();
            services.AddTransient<IStatisticVmService, StatisticVmService>();
        }
    }
}

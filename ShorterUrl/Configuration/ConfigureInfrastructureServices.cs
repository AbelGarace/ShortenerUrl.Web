using ShortenerUrl.Web.Infrastructure.Interfaces;
using ShortenerUrl.Web.Infrastructure.Services;

namespace ShortenerUrl.Web.Configuration
{
    public class ConfigureInfrastructureServices
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IHttpHelper, HttpHelper>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IShortenerApiService, ShortenerApiService>();
        }
    }
}

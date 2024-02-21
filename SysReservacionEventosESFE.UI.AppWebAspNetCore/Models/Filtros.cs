using Microsoft.Extensions.DependencyInjection;
using SysReservacionEventosESFE.UI.AppWebAspNetCore.Models;

namespace SysReservacionEventosESFE.UI.AppWebAspNetCore
{
    public static class StartupConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(AuthorizeUser)); // Añadir el filtro de autorización globalmente
            });
        }
    }
}


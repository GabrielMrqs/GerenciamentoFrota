using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciamentoFrota.Infra.Shared.Extensions
{
    public static class CacheExtensions
    {
        public static void ConfigureRedis(this WebApplicationBuilder builder)
        {
            builder.Services.AddStackExchangeRedisCache(config =>
            {
                config.Configuration = "host.docker.internal";
                config.InstanceName = "Frota";
            });
        } 
    }
}

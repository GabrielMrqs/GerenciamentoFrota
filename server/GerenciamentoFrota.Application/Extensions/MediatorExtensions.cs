using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GerenciamentoFrota.Application.Extensions
{
    public static class MediatorExtensions
    {
        public static void RegisterRequestHandlers(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
        }
    }
}

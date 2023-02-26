using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GerenciamentoFrota.Application.Extensions
{
    public static class FluentValidatorExtensions
    {
        public static void RegisterValidationHandlers(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}

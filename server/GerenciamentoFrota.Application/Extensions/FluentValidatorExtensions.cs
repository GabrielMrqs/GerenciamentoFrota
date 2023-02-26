using FluentValidation;
using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace GerenciamentoFrota.Application.Extensions
{
    public static class FluentValidatorExtensions
    {
        public static void RegisterValidationHandlers(this WebApplicationBuilder builder)
        {
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}

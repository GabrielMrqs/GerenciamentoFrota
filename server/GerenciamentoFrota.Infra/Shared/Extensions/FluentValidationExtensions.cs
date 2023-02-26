using FluentValidation.Results;

namespace GerenciamentoFrota.Infra.Shared.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IEnumerable<string> ErrorsOnly(this List<ValidationFailure> failures)
        {
            return failures.Select(x => x.ErrorMessage);
        }
    }
}

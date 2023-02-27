using FluentValidation;
using GerenciamentoFrota.Domain.Shared;
using GerenciamentoFrota.Infra.Veiculos;
using MediatR;

namespace GerenciamentoFrota.Application.Veiculos
{
    public class ExcluirVeiculoHandler : IRequestHandler<ExcluirVeiculoCommand, Result<Exception, Unit>>
    {
        private readonly IVeiculoService _service;

        public ExcluirVeiculoHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<Result<Exception, Unit>> Handle(ExcluirVeiculoCommand request, CancellationToken cancellationToken)
        {
            var chassi = request.Chassi;

            var jaExisteChassi = await _service.ExisteVeiculoPeloChassi(chassi);

            if (!jaExisteChassi)
            {
                return new Exception($"Não foi encontrado veículo com o chassi: {chassi}");
            }

            await _service.ExcluirVeiculo(chassi);

            return Unit.Task.Result;
        }
    }

    public record ExcluirVeiculoCommand(string Chassi) : IRequest<Result<Exception, Unit>>;

    public class ExcluirVeiculoCommandValidator : AbstractValidator<ExcluirVeiculoCommand>
    {
        public ExcluirVeiculoCommandValidator()
        {
            RuleFor(x => x.Chassi)
                .Length(17).WithMessage("O chassi deve ter 17 caracteres");
        }
    }
}

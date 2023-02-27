using FluentValidation;
using GerenciamentoFrota.Domain.Veiculos;
using GerenciamentoFrota.Domain.Shared;
using GerenciamentoFrota.Infra.Veiculos;
using MediatR;

namespace GerenciamentoFrota.Application.Veiculos
{
    public class VisualizarVeiculoPorChassiHandler : IRequestHandler<VisualizarVeiculoPorChassiCommand, Result<Exception, Veiculo>>
    {
        private readonly IVeiculoService _service;

        public VisualizarVeiculoPorChassiHandler(IVeiculoService service)
        {
            _service = service;
        }

        public async Task<Result<Exception, Veiculo>> Handle(VisualizarVeiculoPorChassiCommand request, CancellationToken cancellationToken)
        {
            var chassi = request.Chassi;

            var veiculo = await _service.ObterVeiculoPeloChassi(chassi);

            if (veiculo is null)
            {
                return new Exception($"Não foi encontrado veículo com o chassi: {chassi}");
            }

            return veiculo;
        }
    }
    public record VisualizarVeiculoPorChassiCommand(string Chassi) : IRequest<Result<Exception, Veiculo>>;

    public class VisualizarVeiculoPorChassiCommandValidator : AbstractValidator<VisualizarVeiculoPorChassiCommand>
    {
        public VisualizarVeiculoPorChassiCommandValidator()
        {
            RuleFor(x => x.Chassi)
                .Length(17).WithMessage("O chassi deve ter 17 caracteres");
        }
    }
}

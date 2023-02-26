using FluentValidation;
using GerenciamentoFrota.Domain.Veiculos;
using GerenciamentoFrota.Domain.Shared;
using GerenciamentoFrota.Infra.Veiculos;
using MediatR;

namespace GerenciamentoFrota.Application.Veiculos
{
    public class VisualizarVeiculoPorChassiHandler : IRequestHandler<VisualizarVeiculoCommandPorChassi, Result<Exception, Veiculo>>
    {
        private readonly VeiculoService _service;

        public VisualizarVeiculoPorChassiHandler(VeiculoService service)
        {
            _service = service;
        }

        public async Task<Result<Exception, Veiculo>> Handle(VisualizarVeiculoCommandPorChassi request, CancellationToken cancellationToken)
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
    public record VisualizarVeiculoCommandPorChassi(string Chassi) : IRequest<Result<Exception, Veiculo>>;

    public class VisualizarVeiculoPorChassiCommandValidator : AbstractValidator<VisualizarVeiculoCommandPorChassi>
    {
        public VisualizarVeiculoPorChassiCommandValidator()
        {
            RuleFor(x => x.Chassi)
                .Length(17).WithMessage("O chassi deve ter 17 caracteres");
        }
    }
}

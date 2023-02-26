using GerenciamentoFrota.Domain.Veiculos;
using GerenciamentoFrota.Domain.Shared;
using GerenciamentoFrota.Infra.Veiculos;
using MediatR;

namespace GerenciamentoFrota.Application.Veiculos
{
    public class VisualizarTodosVeiculosHandler : IRequestHandler<VisualizarTodosVeiculosCommand, Result<Exception, List<Veiculo>>>
    {
        private readonly IVeiculoService _service;

        public VisualizarTodosVeiculosHandler(IVeiculoService service)
        {
            _service = service;
        }
        public async Task<Result<Exception, List<Veiculo>>> Handle(VisualizarTodosVeiculosCommand request, CancellationToken cancellationToken)
        {
            return await _service.ObterTodosVeiculos();
        }
    }

    public record VisualizarTodosVeiculosCommand : IRequest<Result<Exception, List<Veiculo>>>;
}

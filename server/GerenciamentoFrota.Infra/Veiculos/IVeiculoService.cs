using GerenciamentoFrota.Domain.Shared;
using GerenciamentoFrota.Domain.Veiculos;

namespace GerenciamentoFrota.Infra.Veiculos
{
    public interface IVeiculoService
    {
        Task AdicionarVeiculo(Veiculo veiculo);
        Task EditarVeiculo(Veiculo veiculo);
        Task ExcluirVeiculo(string chassi);
        Task<bool> ExisteVeiculoPeloChassi(string chassi);
        Task<Result<Exception, List<Veiculo>>> ObterTodosVeiculos();
        Task<Veiculo> ObterVeiculoPeloChassi(string chassi);
    }
}
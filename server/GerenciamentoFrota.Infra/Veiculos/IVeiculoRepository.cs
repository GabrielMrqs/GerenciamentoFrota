using GerenciamentoFrota.Domain.Veiculos;
using GerenciamentoFrota.Infra.Shared;

namespace GerenciamentoFrota.Infra.Veiculos
{
    public interface IVeiculoRepository : IMongoRepository<Veiculo>
    {
        Task<Veiculo> ObterPeloChassi(string chassi);
    }
}
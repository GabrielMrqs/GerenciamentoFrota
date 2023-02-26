using GerenciamentoFrota.Domain.Veiculos;
using GerenciamentoFrota.Infra.Shared;
using MongoDB.Driver;

namespace GerenciamentoFrota.Infra.Veiculos
{
    public class VeiculoRepository : MongoRepository<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository(IMongoClient client) : base(client)
        {
        }

        protected override string CollectionName => "Veículos";

        public async Task<Veiculo> ObterPeloChassi(string chassi)
        {
            var filter = Builders<Veiculo>.Filter.Eq(x => x.Chassi, chassi.ToLower());
            return await ObterPorPropriedade(filter);
        }
    }
}

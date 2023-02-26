using GerenciamentoFrota.Domain.Veiculos;
using GerenciamentoFrota.Domain.Shared;
using GerenciamentoFrota.Infra.Shared;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;

namespace GerenciamentoFrota.Infra.Veiculos
{
    public class VeiculoService : BaseService<Veiculo>, IVeiculoService
    {
        private readonly IVeiculoRepository _repository;

        protected override string Pasta => "Veiculos";

        public VeiculoService(IVeiculoRepository repository, IDistributedCache cache) : base(cache)
        {
            _repository = repository;
        }

        public async Task<Veiculo> ObterVeiculoPeloChassi(string chassi)
        {
            return await GetFromCache(
                "veiculos",
                chassi,
                async () => await _repository.ObterPeloChassi(chassi)
                );
        }

        public async Task<bool> ExisteVeiculoPeloChassi(string chassi)
        {
            var veiculo = await GetFromCache(
                "veiculos",
                chassi,
                async () => await _repository.ObterPeloChassi(chassi)
                );

            return veiculo is not null;
        }

        public async Task<Result<Exception, List<Veiculo>>> ObterTodosVeiculos()
        {
            var veiculos = await GetFromCache(
                "veiculos",
                "*",
                async () => await _repository.ObterTodos()
                );

            return veiculos;
        }

        public async Task AdicionarVeiculo(Veiculo veiculo)
        {
            veiculo.Chassi = veiculo.Chassi.ToLower();

            await _repository.Adicionar(veiculo);

            await RefreshCache("veiculos", "*");
        }

        public async Task ExcluirVeiculo(string chassi)
        {
            var filter = Builders<Veiculo>.Filter.Eq(x => x.Chassi, chassi.ToLower());

            await _repository.Deletar(filter);

            await RefreshCache("veiculos", chassi);

            await RefreshCache("veiculos", "*");
        }

        public async Task EditarVeiculo(Veiculo veiculo)
        {
            var filter = Builders<Veiculo>.Filter.Eq(x => x.Chassi, veiculo.Chassi.ToLower());

            await _repository.Atualizar(veiculo, filter);

            await RefreshCache("veiculos", veiculo.Chassi);

            await RefreshCache("veiculos", "*");
        }
    }
}

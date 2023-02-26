using MongoDB.Driver;

namespace GerenciamentoFrota.Infra.Shared
{
    public interface IMongoRepository<T>
    {
        Task Adicionar(T entity);
        Task Atualizar(T entity, FilterDefinition<T> filter);
        Task Deletar(FilterDefinition<T> filter);
        Task<T?> ObterPorPropriedade(FilterDefinition<T> filter);
        Task<List<T>> ObterTodos();
    }
}
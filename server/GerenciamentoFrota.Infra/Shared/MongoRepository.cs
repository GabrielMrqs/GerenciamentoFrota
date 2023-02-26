using MongoDB.Driver;

namespace GerenciamentoFrota.Infra.Shared
{
    public abstract class MongoRepository<T> : IMongoRepository<T>
    {
        public MongoRepository(IMongoClient client)
        {
            var database = client.GetDatabase(DatabaseName);
            Collection = database.GetCollection<T>(CollectionName);
        }

        private const string DatabaseName = "Frota";

        protected IMongoCollection<T> Collection { get; }

        protected abstract string CollectionName { get; }

        public async Task Adicionar(T entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public async Task<T?> ObterPorPropriedade(FilterDefinition<T> filter)
        {
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObterTodos()
        {
            var filter = Builders<T>.Filter.Empty;
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task Atualizar(T entity, FilterDefinition<T> filter)
        {
            await Collection.ReplaceOneAsync(filter, entity);
        }

        public async Task Deletar(FilterDefinition<T> filter)
        {
            await Collection.DeleteOneAsync(filter);
        }
    }
}
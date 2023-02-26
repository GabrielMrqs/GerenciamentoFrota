using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace GerenciamentoFrota.Infra.Shared
{
    public abstract class BaseService<T>
    {
        protected abstract string Pasta { get; }
        private readonly IDistributedCache _cache;
        private readonly string _keyFmt = ":{0}:{1}";

        public BaseService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<TResult> GetFromCache<TResult>(string key, string val, Func<Task<TResult>> func)
        {
            var cacheKey = ObterCacheKey(key, val);

            var cached = await _cache.GetStringAsync(cacheKey);

            if (PossuiCache(cached))
            {
                return JsonConvert.DeserializeObject<TResult>(cached);
            }

            var result = await func();

            var data = JsonConvert.SerializeObject(result);

            if (result is not null)
            {
                await _cache.SetStringAsync(cacheKey, data);
            }

            return JsonConvert.DeserializeObject<TResult>(data);
        }

        public async Task RefreshCache(string key, string val)
        {
            var cacheKey = ObterCacheKey(key, val);

            await _cache.RemoveAsync(cacheKey);
        }

        private bool PossuiCache(string cache)
        {
            return !string.IsNullOrEmpty(cache);
        }

        private string ObterCacheKey(string key, string val)
        {
            return $":{Pasta}{string.Format(_keyFmt, key, val)}";
        }
    }
}

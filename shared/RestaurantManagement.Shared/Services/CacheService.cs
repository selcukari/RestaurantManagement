using System.Runtime.Caching;

namespace RestaurantManagement.Shared.Services
{
    public class CacheService : ICacheService
    {
        private static readonly MemoryCache _cache = MemoryCache.Default;

        public CacheService()
        {
            
        }

        public T Get<T>(string key)
        {
            if (_cache.Contains(key))
            {
                return (T)_cache.Get(key);
            }
            return default;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void ClearAll()
        {
            // MemoryCache'i doğru şekilde temizle
            var keys = new List<string>();
            foreach (var element in _cache)
            {
                keys.Add(element.Key);
            }

            foreach (var key in keys)
            {
                _cache.Remove(key);
            }

            var countAfter = _cache.GetCount();
        }

        public void RemoveByPrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                return;

            var keysToRemove = _cache
                .Where(kvp => kvp.Key.StartsWith(prefix))
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
            }
        }

        public void Set<T>(string key, T value, TimeSpan? expiration = null)
        {
            var expire = expiration ?? TimeSpan.FromDays(1);

            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now.Add(expire)
            };

            _cache.Set(key, value, policy);
        }
    }
}

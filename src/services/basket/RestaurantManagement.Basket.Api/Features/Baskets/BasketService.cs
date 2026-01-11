using Microsoft.Extensions.Caching.Distributed;
using RestaurantManagement.Basket.Api.Const;
using RestaurantManagement.Shared.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace RestaurantManagement.Basket.Api.Features.Baskets
{
    public class BasketService(IIdentityService identityService, IDistributedCache distributedCache, IConnectionMultiplexer redis)
    {
        private string GetCacheKey()
        {
            return string.Format(BasketConst.BasketCacheKey, identityService.UserId);
        }

        private string GetCacheKey(Guid userId)
        {
            return string.Format(BasketConst.BasketCacheKey, userId);
        }

        public Task<string?> GetBasketFromCache(CancellationToken cancellationToken)
        {
            return distributedCache.GetStringAsync(GetCacheKey(), cancellationToken);
        }

        public async Task CreateBasketCacheAsync(Data.Basket basket, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(GetCacheKey(), basketAsString, cancellationToken);
        }

        public async Task DeleteBasket(Guid userId)
        {
            await distributedCache.RemoveAsync(GetCacheKey(userId));
        }
        public async Task DeleteAllBasketsFastAsync()
        {
            var server = redis.GetServer(redis.GetEndPoints().First());
            var keys = server.Keys(pattern: "*basket:*").ToArray();

            if (keys.Any())
            {
                var db = redis.GetDatabase();
                // Toplu silme (Dizi olarak gönderilir)
                await db.KeyDeleteAsync(keys);
            }
        }
        public async Task DeleteBasketsByProductId(Guid productId)
        {
            // 1. Redis sunucusuna bağlan (Anahtarları taramak için)
            var server = redis.GetServer(redis.GetEndPoints().First());

            // 2. Tüm sepet anahtarlarını bul (Örn: "basket:*")
            var pattern = "basket:*";
            var keys = server.Keys(pattern: pattern).ToList();

            foreach (var key in keys)
            {
                // 3. Sepet içeriğini oku
                var basketJson = await distributedCache.GetStringAsync(key);
                if (string.IsNullOrEmpty(basketJson)) continue;

                var basket = JsonSerializer.Deserialize<Data.Basket>(basketJson);

                // 4. Eğer sepetin içinde silinmesi istenen ProductId varsa sepeti komple sil
                if (basket != null && basket.Items.Any(x => x.Id == productId))
                {
                    await distributedCache.RemoveAsync(key);
                }
            }
        }
    }
}

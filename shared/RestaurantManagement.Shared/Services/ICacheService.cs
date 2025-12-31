using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Shared.Services
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan? expiration = null);
        void Remove(string key);
        void ClearAll();
        void RemoveByPrefix(string key);
    }
}

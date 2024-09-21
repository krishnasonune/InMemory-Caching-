
using Microsoft.Extensions.Caching.Memory;

public class CacheService : ICacheService
{
    public IMemoryCache _memoryCache;
    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }
    public T Get<T>(string key)
    {
        if (!string.IsNullOrEmpty(key))
        {
            _memoryCache.TryGetValue<T>(key, out T? value);
            return value;
        }
        else
        {
            return default(T)!;
        }
    }

    public bool RemoveData(string key)
    {
        if (!string.IsNullOrEmpty(key))
        {
            _memoryCache.Remove(key);
            return true;
        }
        return false;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expiryTime)
    {
        if (!string.IsNullOrEmpty(key))
        {
            _memoryCache.Set<T>(key, value, expiryTime);
            return true;
        }
        return false;
    }
}
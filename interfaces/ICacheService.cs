public interface ICacheService
{
    T Get<T> (string key);
    bool RemoveData(string key);
    bool SetData<T>(string key, T value, DateTimeOffset expiryTime);
}
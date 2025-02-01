namespace CommonServicesLib.Contracts
{
    public interface ICacheService
    {
        T GetCache<T>(string key);
        void SetCache<T>(string key, T value, TimeSpan expiration);
    }
}
using System.Text.Json;
using Amazon.SecretsManager.Extensions.Caching;

namespace AccesoAlimentario.Web.SecretRetrieve;

public class SecretRetrieve
{
    private SecretsManagerCache _cache;

    public SecretRetrieve()
    {
        var cacheConfiguration = new SecretCacheConfiguration
        {
            CacheItemTTL = 86400000
        };
        _cache = new SecretsManagerCache(cacheConfiguration);
    }

    public string GetSecret(string secretName)
    {
        var secret = _cache.GetSecretString(secretName);
        return secret.Result;
    }

    public T? GetSecretAs<T>(string secretName) where T : class
    {
        var secretString = GetSecret(secretName);
        return JsonSerializer.Deserialize<T>(secretString);
    }
}
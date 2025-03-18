﻿using System.Text.Json;
using CoffeePeek.Infrastructure.Cache.Interfaces;
using StackExchange.Redis;

namespace CoffeePeek.Infrastructure.Cache;

public class RedisService(IConnectionMultiplexer redis) : IRedisService
{
    private readonly IDatabase _db = redis.GetDatabase();

    public async Task<T> GetAsync<T>(string key)
    {
        var value = await _db.StringGetAsync(key);
        return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value);
    }
    
    public async Task<(bool success, T value)> TryGetAsync<T>(string key)
    {
        try
        {
            var redisValue = await _db.StringGetAsync(key);

            if (redisValue.IsNullOrEmpty)
            {
                return (false, default)!;
            }

            var value = JsonSerializer.Deserialize<T>(redisValue!);
            return (value != null, value)!;
        }
        catch (JsonException)
        {
            return (false, default)!;
        }
        catch (RedisConnectionException)
        {
            return (false, default)!;
        }
        catch (Exception)
        {
            return (false, default)!;
        }
    }
    
    public async Task<T> GetAsyncById<T>(string id)
    {
        var key = $"{nameof(T)}-{id}";
        
        var value = await _db.StringGetAsync(key);
        
        return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        await _db.StringSetAsync(key, JsonSerializer.Serialize(value), expiry);
    }

    public async Task RemoveAsync(string key)
    {
        await _db.KeyDeleteAsync(key);
    }
}
using StackExchange.Redis;
using AvaRedisAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaRedisAssistant.Services;

public class RedisService : IDisposable
{
    private ConnectionMultiplexer? _connection;
    private IDatabase? _database;
    private int _currentDatabase = 0;
    
    public bool IsConnected => _connection?.IsConnected ?? false;
    
    public async Task<bool> ConnectAsync(RedisConnection connection)
    {
        try
        {
            Disconnect();
            
            var options = ConfigurationOptions.Parse(connection.ConnectionString);
            options.AbortOnConnectFail = false;
            options.ConnectTimeout = 5000;
            options.AllowAdmin = true;  // Enable admin mode for INFO and CONFIG commands
            
            _connection = await ConnectionMultiplexer.ConnectAsync(options);
            _database = _connection.GetDatabase(connection.Database);
            _currentDatabase = connection.Database;
            
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Connection error: {ex.Message}");
            return false;
        }
    }
    
    public void Disconnect()
    {
        _connection?.Dispose();
        _connection = null;
        _database = null;
        _currentDatabase = 0;
    }
    
    public async Task<List<RedisKeyInfo>> GetKeysAsync(string pattern = "*", int maxKeys = 1000)
    {
        if (_connection == null || _database == null)
            return new List<RedisKeyInfo>();
        
        var keys = new List<RedisKeyInfo>();
        var server = _connection.GetServer(_connection.GetEndPoints().First());
        
        await foreach (var key in server.KeysAsync(database: _currentDatabase, pattern: pattern, pageSize: maxKeys))
        {
            var keyInfo = new RedisKeyInfo
            {
                Key = key.ToString(),
                Type = await _database.KeyTypeAsync(key)
            };
            
            var ttl = await _database.KeyTimeToLiveAsync(key);
            if (ttl.HasValue)
                keyInfo.TimeToLive = ttl.Value;
            
            keys.Add(keyInfo);
            
            if (keys.Count >= maxKeys)
                break;
        }
        
        return keys;
    }
    
    public async Task<string?> GetValueAsync(string key)
    {
        if (_database == null)
            return null;
        
        var type = await _database.KeyTypeAsync(key);
        
        return type switch
        {
            RedisType.String => await _database.StringGetAsync(key),
            RedisType.List => string.Join("\n", (await _database.ListRangeAsync(key)).Select(v => v.ToString())),
            RedisType.Set => string.Join("\n", (await _database.SetMembersAsync(key)).Select(v => v.ToString())),
            RedisType.Hash => string.Join("\n", (await _database.HashGetAllAsync(key)).Select(e => $"{e.Name}: {e.Value}")),
            RedisType.SortedSet => string.Join("\n", (await _database.SortedSetRangeByRankWithScoresAsync(key)).Select(e => $"{e.Element}: {e.Score}")),
            _ => null
        };
    }
    
    public async Task<bool> SetValueAsync(string key, string value)
    {
        if (_database == null)
            return false;
        
        try
        {
            await _database.StringSetAsync(key, value);
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Set value error: {ex.Message}");
            return false;
        }
    }
    
    public async Task<bool> DeleteKeyAsync(string key)
    {
        if (_database == null)
            return false;
        
        return await _database.KeyDeleteAsync(key);
    }
    
    public async Task<RedisServerInfo?> GetServerInfoAsync()
    {
        if (_connection == null)
            return null;
        
        try
        {
            var server = _connection.GetServer(_connection.GetEndPoints().First());
            var info = await server.InfoAsync();
            
            var serverInfo = new RedisServerInfo();
            
            foreach (var section in info)
            {
                foreach (var item in section)
                {
                    switch (item.Key.ToLower())
                    {
                        case "redis_version":
                            serverInfo.Version = item.Value;
                            break;
                        case "os":
                            serverInfo.OperatingSystem = item.Value;
                            break;
                        case "used_memory":
                            if (long.TryParse(item.Value, out var mem))
                                serverInfo.UsedMemory = mem;
                            break;
                        case "connected_clients":
                            if (int.TryParse(item.Value, out var clients))
                                serverInfo.ConnectedClients = clients;
                            break;
                        case "total_commands_processed":
                            if (long.TryParse(item.Value, out var commands))
                                serverInfo.TotalCommandsProcessed = commands;
                            break;
                        case "uptime_in_seconds":
                            if (double.TryParse(item.Value, out var uptime))
                                serverInfo.Uptime = uptime;
                            break;
                    }
                }
            }
            
            serverInfo.TotalKeys = await server.DatabaseSizeAsync(_currentDatabase);
            
            return serverInfo;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Get server info error: {ex.Message}");
            return null;
        }
    }
    
    public async Task<List<int>> GetAvailableDatabasesAsync()
    {
        if (_connection == null)
            return new List<int>();
        
        try
        {
            var server = _connection.GetServer(_connection.GetEndPoints().First());
            var config = await server.ConfigGetAsync("databases");
            
            // Default to 16 databases if config is not available
            int databaseCount = 16;
            if (config.Length > 0 && int.TryParse(config[0].Value, out var count))
            {
                databaseCount = count;
            }
            
            var databases = new List<int>();
            for (int i = 0; i < databaseCount; i++)
            {
                databases.Add(i);
            }
            
            return databases;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Get databases error: {ex.Message}");
            // Return default list 0-15 if there's an error
            return Enumerable.Range(0, 16).ToList();
        }
    }
    
    public void Dispose()
    {
        Disconnect();
    }
}

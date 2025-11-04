using StackExchange.Redis;
using System;

namespace AvaRedisAssistant.Models;

public class RedisKeyInfo
{
    public string Key { get; set; } = string.Empty;
    public RedisType Type { get; set; }
    public long? Size { get; set; }
    public TimeSpan? TimeToLive { get; set; }
}

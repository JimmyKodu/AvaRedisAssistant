using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AvaRedisAssistant.Services;
using AvaRedisAssistant.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace AvaRedisAssistant.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IDisposable
{
    private readonly RedisService _redisService;
    
    [ObservableProperty]
    private string _connectionName = "Local Redis";
    
    [ObservableProperty]
    private string _host = "localhost";
    
    [ObservableProperty]
    private int _port = 6379;
    
    [ObservableProperty]
    private string _password = string.Empty;
    
    [ObservableProperty]
    private int _database = 0;
    
    [ObservableProperty]
    private ObservableCollection<int> _availableDatabases = new(Enumerable.Range(0, 16));
    
    [ObservableProperty]
    private bool _isConnected = false;
    
    [ObservableProperty]
    private string _statusMessage = "Not connected";
    
    [ObservableProperty]
    private string _searchPattern = "*";
    
    [ObservableProperty]
    private ObservableCollection<RedisKeyInfo> _keys = new();
    
    [ObservableProperty]
    private RedisKeyInfo? _selectedKey;
    
    [ObservableProperty]
    private string _keyValue = string.Empty;
    
    [ObservableProperty]
    private string _newKey = string.Empty;
    
    [ObservableProperty]
    private string _newValue = string.Empty;
    
    [ObservableProperty]
    private RedisServerInfo? _serverInfo;
    
    public MainWindowViewModel()
    {
        _redisService = new RedisService();
    }
    
    [RelayCommand]
    private async Task ConnectAsync()
    {
        StatusMessage = "Connecting...";
        
        var connection = new RedisConnection
        {
            Name = ConnectionName,
            Host = Host,
            Port = Port,
            Password = string.IsNullOrEmpty(Password) ? null : Password,
            Database = Database
        };
        
        var connected = await _redisService.ConnectAsync(connection);
        IsConnected = connected;
        StatusMessage = connected ? "Connected" : "Connection failed";
        
        if (connected)
        {
            await LoadAvailableDatabasesAsync();
            await LoadKeysAsync();
            await LoadServerInfoAsync();
        }
    }
    
    [RelayCommand]
    private void Disconnect()
    {
        _redisService.Disconnect();
        IsConnected = false;
        StatusMessage = "Disconnected";
        Keys.Clear();
        ServerInfo = null;
    }
    
    [RelayCommand]
    private async Task LoadKeysAsync()
    {
        if (!IsConnected)
            return;
        
        var keys = await _redisService.GetKeysAsync(SearchPattern, 1000);
        Keys.Clear();
        foreach (var key in keys)
        {
            Keys.Add(key);
        }
        StatusMessage = $"Loaded {keys.Count} keys";
    }
    
    [RelayCommand]
    private async Task LoadKeyValueAsync()
    {
        if (SelectedKey == null)
            return;
        
        var value = await _redisService.GetValueAsync(SelectedKey.Key);
        KeyValue = value ?? string.Empty;
    }
    
    [RelayCommand]
    private async Task AddKeyAsync()
    {
        if (string.IsNullOrEmpty(NewKey))
            return;
        
        var success = await _redisService.SetValueAsync(NewKey, NewValue);
        if (success)
        {
            StatusMessage = $"Key '{NewKey}' added successfully";
            NewKey = string.Empty;
            NewValue = string.Empty;
            await LoadKeysAsync();
        }
        else
        {
            StatusMessage = "Failed to add key";
        }
    }
    
    [RelayCommand]
    private async Task DeleteKeyAsync()
    {
        if (SelectedKey == null)
            return;
        
        var success = await _redisService.DeleteKeyAsync(SelectedKey.Key);
        if (success)
        {
            StatusMessage = $"Key '{SelectedKey.Key}' deleted";
            await LoadKeysAsync();
            KeyValue = string.Empty;
        }
        else
        {
            StatusMessage = "Failed to delete key";
        }
    }
    
    [RelayCommand]
    private async Task LoadServerInfoAsync()
    {
        if (!IsConnected)
            return;
        
        ServerInfo = await _redisService.GetServerInfoAsync();
    }
    
    [RelayCommand]
    private async Task LoadAvailableDatabasesAsync()
    {
        if (!IsConnected)
            return;
        
        var databases = await _redisService.GetAvailableDatabasesAsync();
        AvailableDatabases = new ObservableCollection<int>(databases);
    }
    
    partial void OnSelectedKeyChanged(RedisKeyInfo? value)
    {
        if (value != null)
        {
            Task.Run(async () =>
            {
                try
                {
                    await LoadKeyValueAsync();
                }
                catch
                {
                    // Silently handle errors - user will see empty value
                }
            });
        }
    }
    
    public void Dispose()
    {
        _redisService?.Dispose();
    }
}

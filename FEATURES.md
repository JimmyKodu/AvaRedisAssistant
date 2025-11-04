# AvaRedisAssistant - Feature Documentation

## Overview
AvaRedisAssistant is a cross-platform desktop application for managing and monitoring Redis databases. Built with Avalonia UI framework, it provides a modern, intuitive interface for Redis operations.

## Main Window Layout

The application window is divided into four main sections:

### 1. Connection Panel (Top)
Located at the top of the window, this panel allows you to:
- **Connection Name**: Give your connection a friendly name (default: "Local Redis")
- **Host**: Enter the Redis server hostname (default: "localhost")
- **Port**: Specify the Redis port (default: 6379)
- **Password**: Enter password if Redis requires authentication (optional)
- **Connect/Disconnect Buttons**: Control the connection state

### 2. Keys Browser (Left Panel - 300px width)
This panel displays all Redis keys and includes:
- **Title**: "Redis Keys" header
- **Search Box**: Filter keys using Redis patterns (*, ?, etc.)
- **Search Button**: 🔍 icon button to refresh the key list
- **Keys List**: Scrollable list showing:
  - Key name (bold)
  - Key type (String, List, Set, Hash, SortedSet) in gray text

### 3. Value Viewer (Center Panel)
The main content area showing:
- **Key Name Header**: Displays the selected key name
- **Value Display**: Read-only text area showing the key's value
  - For String: Shows the raw value
  - For List: Shows all elements (one per line)
  - For Set: Shows all members (one per line)
  - For Hash: Shows field:value pairs (one per line)
  - For SortedSet: Shows element:score pairs (one per line)
- **Delete Key Button**: Remove the selected key from Redis

### 4. Right Panel (350px width)
Split into two sections:

#### Server Info (Top Section)
Displays real-time Redis server statistics:
- **Redis Version**: Current Redis server version
- **Operating System**: OS running Redis
- **Used Memory**: Memory consumption in bytes
- **Total Keys**: Number of keys in the current database
- **Connected Clients**: Active client connections
- **Commands Processed**: Total commands executed
- **Uptime**: Server uptime in seconds
- **Refresh Info Button**: Update server statistics

#### Add New Key (Bottom Section)
Create new Redis keys:
- **Key Input**: Text field for the new key name
- **Value Input**: Multi-line text area for the value
- **Add Key Button**: Submit the new key-value pair

### 5. Status Bar (Bottom)
Displays connection status and operation feedback:
- Connection state ("Connected", "Disconnected", "Connecting...")
- Operation results ("Key added successfully", "Key deleted", etc.)
- Error messages if operations fail

## Key Features

### Connection Management
- Simple connection to local or remote Redis servers
- Password authentication support
- Connection state persistence during session
- Clear visual feedback of connection status

### Key Browsing
- View all keys in the selected database
- Pattern-based search using Redis wildcards
- Display key type for easy identification
- Support for up to 1000 keys (configurable)

### Data Operations
- View values for all Redis data types
- Add new string keys quickly
- Delete unwanted keys
- Real-time value updates

### Monitoring
- Live server statistics
- Memory usage tracking
- Client connection monitoring
- Command throughput metrics

## Technical Details

### Architecture
- **MVVM Pattern**: Clean separation of UI and business logic
- **Reactive UI**: Using CommunityToolkit.Mvvm for data binding
- **Async Operations**: All Redis operations are non-blocking
- **Error Handling**: Graceful handling of connection failures

### Supported Redis Types
- ✅ String
- ✅ List
- ✅ Set
- ✅ Hash
- ✅ Sorted Set

### Platform Support
- ✅ Windows 10/11
- ✅ Linux (Ubuntu, Debian, Fedora, etc.)
- ✅ macOS (10.13+)

## Usage Tips

1. **Pattern Search**: Use Redis patterns like `user:*` to find specific keys
2. **Quick Connect**: Default settings (localhost:6379) work for local Redis
3. **Refresh Data**: Use the refresh buttons to get latest data
4. **Monitor Performance**: Check the Server Info panel for health metrics
5. **Safe Operations**: Delete confirmations coming in future updates

## Future Enhancements (Roadmap)

- Multiple connection profiles
- TTL (Time To Live) editing
- Batch operations
- Export/Import functionality
- Command console
- Key renaming
- Value editing
- Database selector
- Custom themes
- Connection history

## Keyboard Shortcuts (Planned)

- `Ctrl+N`: New key
- `Ctrl+R`: Refresh keys
- `Delete`: Delete selected key
- `F5`: Refresh server info
- `Ctrl+Q`: Disconnect

## Performance Considerations

- The application limits key retrieval to 1000 keys by default
- Large values may take time to load and display
- Memory usage scales with the number of keys loaded
- Recommend using pattern search for databases with many keys

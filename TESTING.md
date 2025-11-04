# Testing Guide for AvaRedisAssistant

## Prerequisites

Before testing the application, ensure you have:

1. **.NET 9.0 SDK** installed
   ```bash
   dotnet --version
   # Should output 9.0.x or higher
   ```

2. **Redis Server** running (one of the following):
   
   ### Option A: Local Redis (Recommended for testing)
   
   **Windows (using Chocolatey):**
   ```bash
   choco install redis-64
   redis-server
   ```
   
   **Linux (Ubuntu/Debian):**
   ```bash
   sudo apt update
   sudo apt install redis-server
   sudo systemctl start redis-server
   sudo systemctl status redis-server
   ```
   
   **macOS (using Homebrew):**
   ```bash
   brew install redis
   brew services start redis
   ```
   
   **Docker (All platforms):**
   ```bash
   docker run -d -p 6379:6379 --name redis redis:latest
   ```

3. **Verify Redis is running:**
   ```bash
   redis-cli ping
   # Should respond with: PONG
   ```

## Building the Application

1. **Clone the repository:**
   ```bash
   git clone https://github.com/JimmyKodu/AvaRedisAssistant.git
   cd AvaRedisAssistant
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Build the project:**
   ```bash
   dotnet build
   ```

4. **Build for Release (optional):**
   ```bash
   dotnet build --configuration Release
   ```

## Running the Application

### Development Mode
```bash
dotnet run
```

### Release Mode
```bash
dotnet run --configuration Release
```

### Run from Binary
```bash
# After building
./bin/Debug/net9.0/AvaRedisAssistant
# or
./bin/Release/net9.0/AvaRedisAssistant
```

## Testing Scenarios

### Test 1: Basic Connection

1. Launch the application
2. Use default values:
   - Host: `localhost`
   - Port: `6379`
3. Click **Connect**
4. **Expected**: Status bar shows "Connected"
5. **Expected**: Keys list populates (may be empty for new Redis)
6. **Expected**: Server Info panel shows Redis version and stats

### Test 2: Connection with Password

1. If your Redis requires a password, set it in Redis config:
   ```bash
   # Edit redis.conf
   requirepass mypassword
   ```
2. Restart Redis
3. In the application:
   - Host: `localhost`
   - Port: `6379`
   - Password: `mypassword`
4. Click **Connect**
5. **Expected**: Successful connection

### Test 3: Add a Simple String Key

1. Ensure connected to Redis
2. In the "Add New Key" panel:
   - Key: `test:string`
   - Value: `Hello, Redis!`
3. Click **Add Key**
4. **Expected**: Status bar shows "Key 'test:string' added successfully"
5. **Expected**: Key appears in the keys list
6. Click the key in the list
7. **Expected**: Value viewer shows "Hello, Redis!"

### Test 4: Add Multiple Keys

Add these keys to test different scenarios:

```bash
# Via Add Key UI or redis-cli:
SET user:1 "John Doe"
SET user:2 "Jane Smith"
SET product:100 "Laptop"
LPUSH cart:123 "item1" "item2" "item3"
SADD tags:tech "redis" "database" "nosql"
HSET user:profile:1 name "Alice" age "30" city "NYC"
```

1. Click the search button (🔍) to refresh
2. **Expected**: All keys appear in the list
3. **Expected**: Each key shows its type (String, List, Set, Hash)

### Test 5: Search with Pattern

1. In the search box, enter: `user:*`
2. Click search (🔍)
3. **Expected**: Only keys starting with "user:" appear

Try other patterns:
- `product:*` - All product keys
- `*:1` - All keys ending with ":1"
- `*` - All keys (default)

### Test 6: View Different Data Types

**String:**
1. Click `test:string` from the list
2. **Expected**: Single line value displayed

**List:**
1. Click a List type key
2. **Expected**: Each element on a new line

**Set:**
1. Click a Set type key
2. **Expected**: Each member on a new line

**Hash:**
1. Click a Hash type key
2. **Expected**: Field:value pairs, one per line

### Test 7: Delete a Key

1. Select any key from the list
2. Click **Delete Key**
3. **Expected**: Status shows "Key '...' deleted"
4. Click search to refresh
5. **Expected**: Deleted key no longer appears in list

### Test 8: Server Monitoring

1. While connected, check Server Info panel
2. **Expected**: Displays:
   - Redis Version (e.g., "7.0.0")
   - Operating System
   - Memory usage (in bytes)
   - Total keys count
   - Connected clients
   - Commands processed
   - Uptime (in seconds)
3. Add/delete some keys
4. Click **Refresh Info**
5. **Expected**: Total keys and commands processed increase

### Test 9: Disconnect and Reconnect

1. Click **Disconnect**
2. **Expected**: 
   - Status shows "Disconnected"
   - Keys list clears
   - Server info clears
   - Connect button enabled, Disconnect disabled
3. Click **Connect** again
4. **Expected**: Reconnects successfully, reloads data

### Test 10: Error Handling

**Wrong Host:**
1. Host: `invalid-host`
2. Port: `6379`
3. Click **Connect**
4. **Expected**: Status shows "Connection failed"

**Wrong Port:**
1. Host: `localhost`
2. Port: `9999`
3. Click **Connect**
4. **Expected**: Status shows "Connection failed"

**Wrong Password:**
1. Set up Redis with password
2. Enter wrong password
3. Click **Connect**
4. **Expected**: Status shows "Connection failed"

### Test 11: Large Dataset

Populate Redis with many keys:
```bash
# Using redis-cli
for i in {1..500}; do redis-cli SET "key:$i" "value$i"; done
```

1. Connect to Redis
2. Use pattern `*` and search
3. **Expected**: Application loads up to 1000 keys
4. **Expected**: List remains responsive
5. **Expected**: Scrolling works smoothly

### Test 12: Special Characters

Test keys with special characters:
```bash
redis-cli SET "key:with:colons" "value1"
redis-cli SET "key-with-dashes" "value2"
redis-cli SET "key_with_underscores" "value3"
redis-cli SET "key with spaces" "value4"
```

1. Search with `*`
2. **Expected**: All keys display correctly
3. Click each key
4. **Expected**: Values display correctly

### Test 13: UTF-8 Characters

Test internationalization:
```bash
redis-cli SET "用户:1" "张三"
redis-cli SET "usuario:1" "José"
redis-cli SET "utilisateur:1" "François"
redis-cli SET "пользователь:1" "Иван"
```

1. Search and view these keys
2. **Expected**: UTF-8 characters display correctly

## Performance Testing

### Load Test
1. Create 10,000+ keys in Redis
2. Connect and load keys
3. Monitor application responsiveness
4. **Expected**: UI remains responsive
5. **Expected**: Only first 1000 keys load (by design)

### Memory Test
1. Monitor application memory usage
2. Load keys, view values, refresh repeatedly
3. **Expected**: No memory leaks
4. **Expected**: Memory stays relatively stable

## Cross-Platform Testing

### Windows
1. Build and run on Windows 10/11
2. Test all scenarios above
3. **Expected**: Native Windows look and feel
4. **Expected**: All features work correctly

### Linux
1. Build and run on Ubuntu/Debian/Fedora
2. Test all scenarios above
3. **Expected**: Native Linux GTK appearance
4. **Expected**: All features work correctly

### macOS
1. Build and run on macOS 10.13+
2. Test all scenarios above
3. **Expected**: Native macOS appearance
4. **Expected**: All features work correctly

## Automated Testing (Future)

Currently, testing is manual. Future enhancements will include:
- Unit tests for RedisService
- Integration tests for ViewModels
- UI automation tests with Avalonia test framework
- CI/CD pipeline with automated tests

## Troubleshooting

### "Connection failed" error
- Verify Redis is running: `redis-cli ping`
- Check host and port are correct
- Verify firewall settings
- Check Redis logs for errors

### Keys not appearing
- Click the search button to refresh
- Verify keys exist: `redis-cli KEYS *`
- Check you're connected to correct database

### Application won't start
- Verify .NET 9.0 SDK installed
- Try clean rebuild: `dotnet clean && dotnet build`
- Check for missing dependencies

### Slow performance
- Reduce number of keys in Redis
- Use more specific search patterns
- Close and reopen application
- Check Redis server performance

## Reporting Issues

When reporting bugs, please include:
1. Operating System and version
2. .NET SDK version (`dotnet --version`)
3. Redis version (`redis-cli --version`)
4. Steps to reproduce
5. Expected vs actual behavior
6. Screenshots if applicable
7. Error messages or logs

## Test Results Template

```markdown
## Test Results

**Date:** YYYY-MM-DD
**Tester:** Your Name
**OS:** Windows/Linux/macOS
**.NET Version:** 9.0.x
**Redis Version:** 7.x.x

| Test Case | Result | Notes |
|-----------|--------|-------|
| Basic Connection | ✅ PASS | |
| Password Auth | ✅ PASS | |
| Add String Key | ✅ PASS | |
| Pattern Search | ✅ PASS | |
| Delete Key | ✅ PASS | |
| Server Info | ✅ PASS | |
| ... | ... | |

**Overall Status:** ✅ All tests passed
```

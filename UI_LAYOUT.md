# AvaRedisAssistant - UI Layout Guide

## Application Window Structure

```
┌──────────────────────────────────────────────────────────────────────────────────────┐
│ Redis Assistant - Desktop Management Tool                                      [_][□][X] │
├──────────────────────────────────────────────────────────────────────────────────────┤
│ ┌────────────────────── Connection Panel ─────────────────────────────────────────┐ │
│ │ Connection: [Local Redis___] Host: [localhost___] Port: [6379] [Connect]      │ │
│ │ Password: [****************]                                     [Disconnect]   │ │
│ └──────────────────────────────────────────────────────────────────────────────────┘ │
│                                                                                        │
│ ┌──────────────┬─────────────────────────────────┬──────────────────────────────┐    │
│ │ Redis Keys   │ Key: user:123                   │ Server Info                  │    │
│ │ ┌──────────┐ │                                 │                              │    │
│ │ │[user:*]🔍│ │ ┌─────────────────────────────┐ │ Version: 7.0.0              │    │
│ │ └──────────┘ │ │ {                           │ │ OS: Linux                   │    │
│ │              │ │   "name": "John Doe",       │ │ Memory: 1,234,567 bytes     │    │
│ │ user:123     │ │   "email": "john@mail.com", │ │ Total Keys: 156             │    │
│ │ String       │ │   "age": 30                 │ │ Clients: 2                  │    │
│ │              │ │ }                           │ │ Commands: 45,678            │    │
│ │ product:456  │ │                             │ │ Uptime: 86,400              │    │
│ │ Hash         │ │                             │ │                             │    │
│ │              │ │                             │ │ [Refresh Info]              │    │
│ │ session:789  │ │                             │ ├─────────────────────────────┤    │
│ │ String       │ │                             │ │ Add New Key                 │    │
│ │              │ └─────────────────────────────┘ │                             │    │
│ │ orders:*     │                                 │ Key: [____________]          │    │
│ │ Set          │ [Delete Key]                    │ Value:                      │    │
│ │              │                                 │ ┌───────────────────────┐   │    │
│ │ cart:abc     │                                 │ │                       │   │    │
│ │ List         │                                 │ │                       │   │    │
│ │              │                                 │ └───────────────────────┘   │    │
│ │ ...          │                                 │ [Add Key]                   │    │
│ └──────────────┴─────────────────────────────────┴──────────────────────────────┘    │
│                                                                                        │
│ ┌──────────────────────── Status Bar ────────────────────────────────────────────┐   │
│ │ Connected - 156 keys loaded                                                    │   │
│ └────────────────────────────────────────────────────────────────────────────────┘   │
└──────────────────────────────────────────────────────────────────────────────────────┘
```

## Panel Descriptions

### 1. Top Connection Panel (Full Width)
- **Purpose**: Manage Redis server connections
- **Components**:
  - Connection Name text field (150px)
  - Host text field (150px)  
  - Port text field (80px)
  - Connect/Disconnect buttons
  - Password field (full width second row)
- **Background**: Accent color border

### 2. Left Panel - Keys Browser (300px width)
- **Purpose**: Browse and search Redis keys
- **Components**:
  - Title: "Redis Keys" (Bold, 16pt)
  - Search input with pattern support
  - Search button (🔍)
  - Scrollable list of keys showing:
    * Key name (bold)
    * Key type (gray, 10pt)
- **Border**: Accent color

### 3. Center Panel - Value Viewer (Flexible width)
- **Purpose**: Display selected key's value
- **Components**:
  - Key name header (Bold, 16pt)
  - Scrollable read-only text area
  - Delete Key button (bottom right)
- **Border**: Accent color

### 4. Right Panel - Info & Actions (350px width)
Split into two sections:

#### Top: Server Info (Expandable)
- **Purpose**: Display Redis server statistics
- **Components**:
  - Title: "Server Info" (Bold, 16pt)
  - Version display
  - OS information
  - Memory usage
  - Key count
  - Client connections
  - Commands processed
  - Uptime
  - Refresh Info button
- **Border**: Accent color

#### Bottom: Add New Key (Fixed height)
- **Purpose**: Create new Redis keys
- **Components**:
  - Title: "Add New Key" (Bold, 16pt)
  - Key name text field
  - Multi-line value text area (60px height)
  - Add Key button (full width)
- **Border**: Accent color

### 5. Bottom Status Bar (Full Width)
- **Purpose**: Show connection status and operation feedback
- **Background**: Accent color
- **Text**: White, bold
- **Examples**:
  - "Not connected"
  - "Connecting..."
  - "Connected"
  - "Key 'user:123' added successfully"
  - "Key 'session:abc' deleted"
  - "Loaded 156 keys"

## Color Scheme

- **Accent Color**: System accent (dynamically from OS theme)
- **Borders**: Accent color, 1px thickness
- **Corner Radius**: 5px for all panels
- **Padding**: 10px for all content areas
- **Margins**: 10px between major sections, 5px between minor elements

## Font Specifications

- **Headers**: Bold, 16pt
- **Normal Text**: Default, 12pt
- **Metadata**: Gray, 10pt
- **Status Bar**: White, Bold

## Responsive Behavior

- **Minimum Window Size**: 1200x700
- **Left Panel**: Fixed 300px
- **Right Panel**: Fixed 350px
- **Center Panel**: Flexible, fills remaining space
- **Lists**: Scrollable when content exceeds panel height

## Interactive Elements

### Buttons
- Connect/Disconnect: Toggle based on connection state
- Search (🔍): Triggers key search with pattern
- Delete Key: Enabled when key is selected
- Refresh Info: Updates server statistics
- Add Key: Submits new key-value pair

### Text Fields
- All support keyboard input
- Password field: Masked with asterisks
- Value fields: Multi-line support with text wrapping

### Lists
- Keys list: Single selection
- Click to view key value
- Shows key type indicator

## Keyboard Support (Future)
- Tab navigation between fields
- Enter to submit forms
- Delete key for selected items
- Ctrl+R to refresh
- Ctrl+N for new key

## Accessibility
- High contrast support
- Screen reader compatible labels
- Keyboard navigation
- Visual state indicators

## Data Binding

All fields use two-way binding:
- Connection fields ↔ ViewModel properties
- Keys list ↔ ObservableCollection
- Selected key ↔ SelectedKey property
- Status message ↔ StatusMessage property
- Server info ↔ ServerInfo object

## Performance Considerations

- Maximum 1000 keys displayed at once
- Lazy loading for key values
- Async operations for all Redis calls
- No blocking UI operations
- Debounced search input (future enhancement)

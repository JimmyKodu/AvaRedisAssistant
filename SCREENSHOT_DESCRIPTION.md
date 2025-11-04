# AvaRedisAssistant - UI Screenshot Description

Since this is a GUI application and cannot be directly run in a headless CI environment, here's a detailed description of what the application looks like when running:

## Main Window Appearance

### Window Title Bar
```
Redis Assistant - Desktop Management Tool                              [_][□][X]
```

### Top Connection Panel (Light blue border, rounded corners)
```
┌─────────────────────────────────────────────────────────────────────────────┐
│ Connection: [Local Redis    ] Host: [localhost  ] Port: [6379] [Connect]   │
│ Password:   [****************]                              [Disconnect]    │
└─────────────────────────────────────────────────────────────────────────────┘
```

### Three-Column Layout

#### Left Panel - Keys Browser (300px, Light blue border)
```
┌──────────────────────┐
│  Redis Keys          │
│  ┌──────────────┐    │
│  │[user:*    ]🔍│    │
│  └──────────────┘    │
│                      │
│  user:123            │
│  String              │
│                      │
│  user:456            │
│  String              │
│                      │
│  product:100         │
│  Hash                │
│                      │
│  cart:abc123         │
│  List                │
│                      │
│  tags:tech           │
│  Set                 │
│                      │
│  scores:game         │
│  Sorted Set          │
│                      │
│  ...                 │
└──────────────────────┘
```

#### Center Panel - Value Viewer (Flexible width, Light blue border)
```
┌─────────────────────────────────────────────────┐
│  Key: user:123                                  │
│                                                 │
│  ┌────────────────────────────────────────────┐│
│  │ {                                          ││
│  │   "id": 123,                              ││
│  │   "name": "John Doe",                     ││
│  │   "email": "john@example.com",            ││
│  │   "age": 30,                              ││
│  │   "active": true                          ││
│  │ }                                          ││
│  │                                            ││
│  │                                            ││
│  │                                            ││
│  └────────────────────────────────────────────┘│
│                                                 │
│                                [Delete Key]     │
└─────────────────────────────────────────────────┘
```

#### Right Panel - Info & Actions (350px)

##### Top: Server Info (Light blue border)
```
┌──────────────────────────────┐
│  Server Info                 │
│                              │
│  Version                     │
│  7.0.15                      │
│                              │
│  Operating System            │
│  Linux 5.15.0-1 x86_64       │
│                              │
│  Used Memory                 │
│  1,234,567 bytes             │
│                              │
│  Total Keys                  │
│  156                         │
│                              │
│  Connected Clients           │
│  2                           │
│                              │
│  Commands Processed          │
│  45,678                      │
│                              │
│  Uptime (seconds)            │
│  86,400                      │
│                              │
│  [Refresh Info]              │
└──────────────────────────────┘
```

##### Bottom: Add New Key (Light blue border)
```
┌──────────────────────────────┐
│  Add New Key                 │
│                              │
│  Key:                        │
│  [____________________]      │
│                              │
│  Value:                      │
│  ┌────────────────────────┐ │
│  │                        │ │
│  │                        │ │
│  └────────────────────────┘ │
│                              │
│  [Add Key]                   │
└──────────────────────────────┘
```

### Bottom Status Bar (Accent color background, white text)
```
┌─────────────────────────────────────────────────────────────────────────────┐
│  Connected - Loaded 156 keys                                                │
└─────────────────────────────────────────────────────────────────────────────┘
```

## Color Scheme

- **Primary Background**: Light gray/white (system theme)
- **Panel Borders**: System accent color (blue by default)
- **Border Width**: 1px
- **Corner Radius**: 5px rounded corners
- **Status Bar**: Solid accent color background with white bold text
- **Headers**: Bold, 16pt font
- **Normal Text**: Regular, 12pt font
- **Metadata**: Gray, 10pt font

## Interactive States

### Buttons
- **Enabled**: Full color, clickable
- **Disabled**: Grayed out, not clickable
- **Hover**: Slight highlight effect

### Connection State
- **Not Connected**: 
  - Connect button: Enabled
  - Disconnect button: Disabled
  - Status: "Not connected" in red/orange
  
- **Connected**:
  - Connect button: Disabled
  - Disconnect button: Enabled
  - Status: "Connected" in green

### List Selection
- **Unselected**: Normal background
- **Selected**: Highlighted with accent color
- **Hover**: Light highlight

## Data Type Visual Indicators

Each key in the list shows:
1. **Key Name** (Bold, black)
2. **Type Badge** (Small, gray text):
   - "String"
   - "List"
   - "Set"
   - "Hash"
   - "Sorted Set"

## Example Screenshots Scenarios

### Scenario 1: Initial Launch
- Connection panel shows default values (localhost:6379)
- All panels empty
- Status bar: "Not connected"
- Connect button enabled

### Scenario 2: Connected State
- Keys list populated with Redis keys
- Selected key shows its value in center panel
- Server info displays Redis statistics
- Status bar: "Connected"
- Various buttons enabled

### Scenario 3: Search Active
- Search box contains pattern (e.g., "user:*")
- Keys list filtered to matching keys only
- Status bar: "Loaded X keys"

### Scenario 4: Adding New Key
- Add Key panel filled with new key and value
- After clicking Add Key:
  - Status bar: "Key 'newkey' added successfully"
  - Key appears in list
  - Input fields cleared

### Scenario 5: Deleting Key
- Key selected in list
- After clicking Delete Key:
  - Status bar: "Key 'oldkey' deleted"
  - Key removed from list
  - Value viewer cleared

## Platform-Specific Appearance

### Windows
- Native Windows 11 rounded corners
- Windows accent color from system settings
- Segoe UI font
- Native window chrome

### Linux
- GTK-styled controls
- System theme colors
- Liberation Sans or system font
- Native window decorations

### macOS
- macOS Big Sur+ rounded corners
- System accent color
- San Francisco font
- Native traffic light buttons

## Responsive Behavior

- Minimum window size: 1200x700
- Left panel: Fixed 300px width
- Right panel: Fixed 350px width
- Center panel: Flexible, expands with window
- Vertical scrolling in all panels when content overflows

## Accessibility Features

- High contrast mode support
- Keyboard navigation (Tab, Enter, Arrow keys)
- Screen reader compatible labels
- Clear focus indicators
- Large click targets (minimum 44x44 pixels)

---

To see the actual application in action, run:
```bash
dotnet run
```

And connect to a Redis server (local or remote).

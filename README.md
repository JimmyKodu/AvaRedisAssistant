# AvaRedisAssistant

用于可视化管理和监控的桌面 Redis GUI，基于 Avalonia 框架开发。

A desktop Redis GUI application for visual management and monitoring, built with Avalonia framework.

## 功能特性 / Features

- 🔌 **连接管理** / Connection Management
  - 支持连接到本地或远程 Redis 服务器
  - 支持密码认证
  - 支持 SSL 连接

- 🔍 **键值浏览** / Key-Value Browsing
  - 查看所有 Redis 键
  - 支持模式搜索（通配符支持）
  - 显示键的类型（String, List, Set, Hash, Sorted Set）
  - 显示键的 TTL（生存时间）

- 📝 **数据操作** / Data Operations
  - 查看键的值
  - 添加新键值对
  - 删除键
  - 支持多种数据类型的显示

- 📊 **服务器监控** / Server Monitoring
  - 显示 Redis 版本信息
  - 显示操作系统信息
  - 内存使用统计
  - 键总数统计
  - 连接客户端数量
  - 已处理命令总数
  - 服务器运行时间

## 技术栈 / Tech Stack

- **.NET 9.0**
- **Avalonia 11.3.8** - 跨平台 UI 框架
- **StackExchange.Redis** - Redis 客户端库
- **CommunityToolkit.Mvvm** - MVVM 框架

## 快速开始 / Quick Start

### 前置要求 / Prerequisites

- .NET 9.0 SDK
- Redis 服务器（本地或远程）

### 构建和运行 / Build and Run

```bash
# 克隆仓库 / Clone the repository
git clone https://github.com/JimmyKodu/AvaRedisAssistant.git
cd AvaRedisAssistant

# 还原依赖 / Restore dependencies
dotnet restore

# 构建项目 / Build the project
dotnet build

# 运行应用 / Run the application
dotnet run
```

### 使用说明 / Usage

1. **连接到 Redis 服务器** / Connect to Redis Server
   - 填写服务器主机名（默认：localhost）
   - 填写端口号（默认：6379）
   - 如需要，填写密码
   - 点击"Connect"按钮

2. **浏览键** / Browse Keys
   - 连接成功后，应用会自动加载键列表
   - 使用搜索框输入模式（如 `user:*`）过滤键
   - 点击键查看其值

3. **添加键** / Add Key
   - 在右侧面板输入键名和值
   - 点击"Add Key"按钮

4. **删除键** / Delete Key
   - 选择一个键
   - 点击"Delete Key"按钮

5. **查看服务器信息** / View Server Info
   - 服务器信息面板显示实时统计数据
   - 点击"Refresh Info"按钮更新信息

## 项目结构 / Project Structure

```
AvaRedisAssistant/
├── Models/              # 数据模型
│   ├── RedisConnection.cs
│   ├── RedisKeyInfo.cs
│   └── RedisServerInfo.cs
├── Services/            # 服务层
│   └── RedisService.cs
├── ViewModels/          # 视图模型
│   ├── MainWindowViewModel.cs
│   └── ViewModelBase.cs
├── Views/               # 视图
│   ├── MainWindow.axaml
│   └── MainWindow.axaml.cs
└── Assets/              # 资源文件
```

## 支持的平台 / Supported Platforms

- ✅ Windows
- ✅ Linux
- ✅ macOS

## 许可证 / License

MIT License

## 贡献 / Contributing

欢迎提交 Issue 和 Pull Request！

Contributions are welcome! Please feel free to submit issues and pull requests.

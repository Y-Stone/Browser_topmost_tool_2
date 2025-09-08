## 浏览器置顶工具（Windows / .NET 8 WinForms）

一个基于 WebView2 的轻量级桌面小工具，可在独立窗口中打开指定网址，并支持窗口置顶、可调透明度、尺寸同步和可选隐藏边框。适合做浮窗浏览、监控看板、直播间/工单面板等场景。

### 功能特性
- **置顶窗口**: 主界面与浏览器窗口均置顶显示。
- **可调透明度**: 支持 0.10 - 1.00（10%-100%）的透明度调整，滑块与输入框双向联动。
- **尺寸控制**: 宽高范围 200 - 10000，可通过滚轮快速增减（步进 10）。
- **边框隐藏**: 可切换是否显示窗口边框（无边框更适合做“贴边浮窗”）。
- **网址直达**: 自动补全协议（未填写时会补 `https://`）。
- **设置持久化**: 退出时可选择保存，存储到本地用户目录。

### 运行环境
- Windows 10/11（x64）
- .NET 8 桌面运行时（框架依赖发布时需要）
- Microsoft Edge WebView2 Runtime（通常系统已预装；若未安装，请前往微软官网安装）

### 快速开始
1. 启动程序（`Tool.exe`）。
2. 在“目标网站”输入框中填入网址，例如 `https://www.example.com`。
3. 在“窗口设置”中调整：
   - 宽 / 高
   - 透明度（输入或拖动滑块）
   - 是否隐藏窗口边框
4. 点击“打开浏览器”后会弹出一个内嵌 WebView2 的浏览器窗口：
   - 窗口置顶显示
   - 与主界面同步透明度和尺寸
5. 可随时点击“关闭浏览器”关闭嵌入窗口。
6. 退出主界面时，选择是否保存当前设置。

### 设置存储位置
应用设置以 JSON 形式保存在：
```
%LOCALAPPDATA%\BrowserTopmostTool\settings.json
```

保存的内容包括：`Width`, `Height`, `Opacity`, `Url`, `HideBorder`。

### 构建与发布
项目使用 .NET 8 WinForms 与 WebView2：

- 还原与构建（Release）：
```powershell
dotnet restore
dotnet build -c Release
```

- 发布（脚本已提供）：
  - `publish_single.bat`：单文件、自包含（Self-contained）win-x64 发布，适合无 .NET 环境机器直接运行。
  - `publish_windows.bat`：同时生成自包含和框架依赖两种产物。

发布输出目录示例：
```
bin\Release\net8.0-windows\win-x64\publish\
```

### 主要依赖
- `Microsoft.Web.WebView2` NuGet 包（`Tool.csproj` 中已引用）

### 目录与代码概览
- `Program.cs`：程序入口，启动 `MainForm`。
- `MainForm.cs` / `MainForm.Designer.cs`：主界面。负责：
  - 读取/保存设置（`SettingsService`）
  - 打开/关闭 `BrowserForm`
  - 与浏览器窗口联动尺寸、透明度、边框显示
- `BrowserForm.cs` / `BrowserForm.Designer.cs`：嵌入 WebView2 的浏览器窗口。
  - 初始化并导航到目标网址
  - 顶置显示、可切换边框、响应大小变化
- `Settings.cs`：设置模型 `AppSettings` 与 `SettingsService`（JSON 存取）。
- `Controls/WheelNumericUpDown.cs`：扩展数值输入，滚轮步进 10，便于快速调节。
- `Tool.csproj`：.NET 8 Windows 窗体项目文件，引用 WebView2。
- `publish_single.bat` / `publish_windows.bat`：发布脚本。

### 常见问题
- 打不开网页或白屏？请确认已安装 WebView2 Runtime，或联网可达。
- 输入网址需以 `http://` 或 `https://` 开头；主界面已做基础校验与自动补全。
- 无边框模式下如需拖动窗口，请临时恢复边框或使用系统快捷方式移动窗口。

### 许可证
根据你的需求自行添加许可证文件（例如 MIT）。



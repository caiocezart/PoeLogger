# PoeLogger

A simple ImGui-based logger class library designed specifically for ExileCore2 plugins. This is not a standalone logging solution and requires ExileCore2 and ImGui.NET to function.

## Features

- ImGui-based visual logging window
- Multiple log levels (Debug, Info, Warning, Error)
- Color-coded log entries
- Thread-safe logging using ConcurrentQueue
- Auto-scrolling to latest log entries
- Maximum entry limit to prevent memory issues
- Clear log functionality
- Seamless integration with ExileCore2's ImGui implementation

## Requirements

- .NET 8.0
- ImGui.NET (1.91.6.1 or higher)
- ExileCore2 environment

## Installation

Add the project as a reference to your ExileCore2 plugin

## Basic Usage

```csharp
// Initialize the logger
var logger = new Logger();

// Log messages
logger.Debug("Debug message");
logger.Info("Info message");
logger.Warning("Warning message");
logger.Error("Error message");

// Render the logger window (typically in your plugin's Render method)
public override void Render()
{
    if (Settings.Debug.ShowWindow)
    {
        logger.Render();
    }
}
```

## Configuration

The logger can be initialized with a custom maximum number of entries:

```csharp
var logger = new Logger(maxEntries: 2000); // Default is 1000
```

## License

MIT License

## Credits

Created for use with ExileCore2 plugins.
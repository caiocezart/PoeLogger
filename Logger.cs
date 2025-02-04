using ImGuiNET;
using System.Collections.Concurrent;

public class Logger
{
    private readonly ConcurrentQueue<LogEntry> _logs;
    private readonly int _maxEntries;
    private bool _autoScroll;
    private readonly Dictionary<LogLevel, System.Numerics.Vector4> _logColors;

    public Logger(int maxEntries = 1000)
    {
        _logs = new ConcurrentQueue<LogEntry>();
        _maxEntries = maxEntries;
        _autoScroll = true;

        _logColors = new Dictionary<LogLevel, System.Numerics.Vector4>
        {
            { LogLevel.Debug, new System.Numerics.Vector4(0.7f, 0.7f, 0.7f, 1.0f) },
            { LogLevel.Info, new System.Numerics.Vector4(1.0f, 1.0f, 1.0f, 1.0f) },
            { LogLevel.Warning, new System.Numerics.Vector4(1.0f, 0.8f, 0.0f, 1.0f) },
            { LogLevel.Error, new System.Numerics.Vector4(1.0f, 0.3f, 0.3f, 1.0f) }
        };
    }

    public void Debug(string message) => Log(message, LogLevel.Debug);
    public void Info(string message) => Log(message, LogLevel.Info);
    public void Warning(string message) => Log(message, LogLevel.Warning);
    public void Error(string message) => Log(message, LogLevel.Error);

    private void Log(string message, LogLevel level)
    {
        _logs.Enqueue(new LogEntry(message, level));
        while (_logs.Count > _maxEntries)
        {
            _logs.TryDequeue(out _);
        }
    }

    public void Render()
    {
        if (ImGui.Begin("Logger"))
        {
            if (ImGui.Button("Clear"))
            {
                while (_logs.TryDequeue(out _)) { }
            }
            
            ImGui.SameLine();
            ImGui.Checkbox("Auto-scroll", ref _autoScroll);

            ImGui.Separator();

            ImGui.BeginChild("ScrollingRegion", new System.Numerics.Vector2(0, -ImGui.GetFrameHeightWithSpacing()), false, ImGuiWindowFlags.HorizontalScrollbar);

            foreach (var entry in _logs)
            {
                ImGui.PushStyleColor(ImGuiCol.Text, _logColors[entry.Level]);
                ImGui.TextUnformatted($"[{entry.Timestamp:HH:mm:ss}] [{entry.Level}] {entry.Message}");
                ImGui.PopStyleColor();
            }

            if (_autoScroll && ImGui.GetScrollY() >= ImGui.GetScrollMaxY())
            {
                ImGui.SetScrollHereY(1.0f);
            }

            ImGui.EndChild();
        }
        ImGui.End();
    }

    public void Clear()
    {
        while (_logs.TryDequeue(out _)) { }
    }
}
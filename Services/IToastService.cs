namespace BlazorPwaTemplate.Services;

public enum ToastLevel
{
    Success,
    Warning,
    Error,
    Info
}

public enum ToastType
{
    Success,
    Warning,
    Error,
    Info
}

public class ToastMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UniqueId { get; set; } = Guid.NewGuid().ToString("N");
    public ToastLevel Level { get; set; }
    public ToastType Type { get; set; }
    public string? Title { get; set; }
    public string Message { get; set; } = string.Empty;
    public int DurationMs { get; set; }
}

public interface IToastService : IAsyncDisposable
{
    event Action<ToastMessage>? OnToastAdded;
    event Action<Guid>? OnToastRemoved;
    void ShowSuccess(string message, string? title = null, int durationMs = 3000);
    void ShowWarning(string message, string? title = null, int durationMs = 4000);
    void ShowError(string message, string? title = null, int durationMs = 5000);
    void ShowInfo(string message, string? title = null, int durationMs = 3000);
    void Success(string message, string? title = null, int durationMs = 3000);
    void Warning(string message, string? title = null, int durationMs = 4000);
    void Error(string message, string? title = null, int durationMs = 5000);
    void Info(string message, string? title = null, int durationMs = 3000);
    void RemoveToast(Guid id);
}

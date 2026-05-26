using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class ToastService : IToastService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public event Action<ToastMessage>? OnToastAdded;
    public event Action<Guid>? OnToastRemoved;

    public ToastService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/toast.module.js").AsTask());
    }

    public void ShowSuccess(string message, string? title = null, int durationMs = 3000)
    {
        NotifyToast(ToastType.Success, ToastLevel.Success, message, title, durationMs);
    }

    public void ShowWarning(string message, string? title = null, int durationMs = 4000)
    {
        NotifyToast(ToastType.Warning, ToastLevel.Warning, message, title, durationMs);
    }

    public void ShowError(string message, string? title = null, int durationMs = 5000)
    {
        NotifyToast(ToastType.Error, ToastLevel.Error, message, title, durationMs);
    }

    public void ShowInfo(string message, string? title = null, int durationMs = 3000)
    {
        NotifyToast(ToastType.Info, ToastLevel.Info, message, title, durationMs);
    }

    public void Success(string message, string? title = null, int durationMs = 3000)
    {
        NotifyToast(ToastType.Success, ToastLevel.Success, message, title, durationMs);
    }

    public void Warning(string message, string? title = null, int durationMs = 4000)
    {
        NotifyToast(ToastType.Warning, ToastLevel.Warning, message, title, durationMs);
    }

    public void Error(string message, string? title = null, int durationMs = 5000)
    {
        NotifyToast(ToastType.Error, ToastLevel.Error, message, title, durationMs);
    }

    public void Info(string message, string? title = null, int durationMs = 3000)
    {
        NotifyToast(ToastType.Info, ToastLevel.Info, message, title, durationMs);
    }

    public void RemoveToast(Guid id)
    {
        OnToastRemoved?.Invoke(id);
    }

    private void NotifyToast(ToastType type, ToastLevel level, string message, string? title, int durationMs)
    {
        var toast = new ToastMessage
        {
            Type = type,
            Level = level,
            Title = title,
            Message = message,
            DurationMs = durationMs
        };

        OnToastAdded?.Invoke(toast);

        _ = LogToJsAsync($"Toast [{level}]: {message}");
    }

    private async Task LogToJsAsync(string msg)
    {
        try
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("log", msg);
        }
        catch
        {
            // Fail silently
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}

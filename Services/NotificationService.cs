using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class NotificationService : INotificationService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public NotificationService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/notification.module.js").AsTask());
    }

    public async Task<string> RequestPermissionAsync()
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<string>("requestPermission");
    }

    public async Task ShowNotificationAsync(string title, string body, string? icon = null, int[]? vibrate = null)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("showNotification", title, body, icon, vibrate);
    }

    public async Task ScheduleLocalNotificationAsync(string title, string body, int delayMs, string? icon = null, int[]? vibrate = null)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("scheduleLocalNotification", title, body, delayMs, icon, vibrate);
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

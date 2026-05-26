namespace BlazorPwaTemplate.Services;

public interface INotificationService : IAsyncDisposable
{
    Task<string> RequestPermissionAsync();
    Task ShowNotificationAsync(string title, string body, string? icon = null, int[]? vibrate = null);
    Task ScheduleLocalNotificationAsync(string title, string body, int delayMs, string? icon = null, int[]? vibrate = null);
}

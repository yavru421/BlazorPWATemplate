namespace BlazorPwaTemplate.Services;

public interface IScreenService : IAsyncDisposable
{
    Task RequestFullscreenAsync();
    Task ExitFullscreenAsync();
    Task<bool> LockOrientationAsync(string orientation);
    Task<bool> RequestWakeLockAsync();
    Task ReleaseWakeLockAsync();
}

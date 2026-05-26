using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class ScreenService : IScreenService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public ScreenService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/screen.module.js").AsTask());
    }

    public async Task RequestFullscreenAsync()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("requestFullscreen");
    }

    public async Task ExitFullscreenAsync()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("exitFullscreen");
    }

    public async Task<bool> LockOrientationAsync(string orientation)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("lockOrientation", orientation);
    }

    public async Task<bool> RequestWakeLockAsync()
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("requestWakeLock");
    }

    public async Task ReleaseWakeLockAsync()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("releaseWakeLock");
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

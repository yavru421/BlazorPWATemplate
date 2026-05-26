using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class HapticsService : IHapticsService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public HapticsService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/haptics.module.js").AsTask());
    }

    public async Task<bool> IsSupportedAsync()
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("isSupported");
    }

    public async Task VibrateAsync(int durationMs)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("vibrate", durationMs);
    }

    public async Task VibratePatternAsync(int[] pattern)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("vibratePattern", pattern);
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

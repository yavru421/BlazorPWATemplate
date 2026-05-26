using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class MotionService : IMotionService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public MotionService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/motion.module.js").AsTask());
    }

    public async Task<bool> RequestPermissionAsync()
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("requestPermission");
    }

    public async Task StartListeningAsync<T>(DotNetObjectReference<T> dotNetHelper, string callbackMethodName) where T : class
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("startListening", dotNetHelper, callbackMethodName);
    }

    public async Task StopListeningAsync()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("stopListening");
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

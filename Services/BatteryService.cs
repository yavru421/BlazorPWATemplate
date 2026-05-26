using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class BatteryService : IBatteryService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public BatteryService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/battery.module.js").AsTask());
    }

    public async Task<bool> IsSupportedAsync()
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("isSupported");
    }

    public async Task<BatteryStatus?> GetStatusAsync()
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<BatteryStatus?>("getStatus");
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

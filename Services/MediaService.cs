using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class MediaService : IMediaService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public MediaService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/media.module.js").AsTask());
    }

    public async Task<bool> IsSupportedAsync()
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("isSupported");
    }

    public async Task<bool> StartMicAsync(string canvasId, DotNetObjectReference<object> dotNetHelper, string callbackMethodName)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("startMic", canvasId, dotNetHelper, callbackMethodName);
    }

    public async Task StopMicAsync()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("stopMic");
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

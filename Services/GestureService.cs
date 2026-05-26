using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class GestureService : IGestureService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public GestureService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/gesture.module.js").AsTask());
    }

    public async Task AttachSwipeAsync<T>(string elementId, DotNetObjectReference<T> dotNetHelper, string callbackMethodName) where T : class
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("attachSwipe", elementId, dotNetHelper, callbackMethodName);
    }

    public async Task AttachPullToRefreshAsync<T>(string elementId, DotNetObjectReference<T> dotNetHelper, string callbackMethodName) where T : class
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("attachPullToRefresh", elementId, dotNetHelper, callbackMethodName);
    }

    public async Task DetachAsync(string elementId)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("detach", elementId);
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

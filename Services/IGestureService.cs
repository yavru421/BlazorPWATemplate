using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public interface IGestureService : IAsyncDisposable
{
    Task AttachSwipeAsync<T>(string elementId, DotNetObjectReference<T> dotNetHelper, string callbackMethodName) where T : class;
    Task AttachPullToRefreshAsync<T>(string elementId, DotNetObjectReference<T> dotNetHelper, string callbackMethodName) where T : class;
    Task DetachAsync(string elementId);
}

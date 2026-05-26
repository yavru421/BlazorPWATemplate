using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public interface IMediaService : IAsyncDisposable
{
    Task<bool> IsSupportedAsync();
    Task<bool> StartMicAsync(string canvasId, DotNetObjectReference<object> dotNetHelper, string callbackMethodName);
    Task StopMicAsync();
}

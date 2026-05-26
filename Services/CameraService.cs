using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class CameraService : ICameraService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public CameraService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/camera.module.js").AsTask());
    }

    public async Task<bool> StartCameraAsync(string videoElementId, bool frontCamera)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("startCamera", videoElementId, frontCamera);
    }

    public async Task<string> CapturePhotoAsync(string videoElementId)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<string>("capturePhoto", videoElementId);
    }

    public async Task StopCameraAsync(string videoElementId)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("stopCamera", videoElementId);
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

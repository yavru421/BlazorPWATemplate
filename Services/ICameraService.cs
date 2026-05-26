namespace BlazorPwaTemplate.Services;

public interface ICameraService : IAsyncDisposable
{
    Task<bool> StartCameraAsync(string videoElementId, bool frontCamera);
    Task<string> CapturePhotoAsync(string videoElementId);
    Task StopCameraAsync(string videoElementId);
}

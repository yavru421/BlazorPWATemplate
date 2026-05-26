using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class MotionData
{
    public double AccelerationX { get; set; }
    public double AccelerationY { get; set; }
    public double AccelerationZ { get; set; }
    public double AccelerationIncludingGravityX { get; set; }
    public double AccelerationIncludingGravityY { get; set; }
    public double AccelerationIncludingGravityZ { get; set; }
    public double RotationRateAlpha { get; set; }
    public double RotationRateBeta { get; set; }
    public double RotationRateGamma { get; set; }
    public double Interval { get; set; }
}

public interface IMotionService : IAsyncDisposable
{
    Task<bool> RequestPermissionAsync();
    Task StartListeningAsync<T>(DotNetObjectReference<T> dotNetHelper, string callbackMethodName) where T : class;
    Task StopListeningAsync();
}

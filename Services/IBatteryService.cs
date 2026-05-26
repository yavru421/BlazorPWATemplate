using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class BatteryStatus
{
    public double Level { get; set; }
    public bool Charging { get; set; }
    public double ChargingTime { get; set; }
    public double DischargingTime { get; set; }
}

public interface IBatteryService : IAsyncDisposable
{
    Task<bool> IsSupportedAsync();
    Task<BatteryStatus?> GetStatusAsync();
    Task StartListeningAsync<T>(DotNetObjectReference<T> dotNetHelper, string callbackMethodName) where T : class;
    Task StopListeningAsync();
}

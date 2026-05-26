using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class GeolocationResult
{
    public bool Success { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Accuracy { get; set; }
    public string? ErrorMessage { get; set; }
}

public interface IGeolocationService : IAsyncDisposable
{
    Task<GeolocationResult> GetCurrentPositionAsync();
    Task<int> WatchPositionAsync<T>(DotNetObjectReference<T> dotNetHelper, string callbackMethodName) where T : class;
    Task ClearWatchAsync(int watchId);
    Task LoadLeafletAsync();
    Task InitMapAsync(string elementId, double lat, double lng);
}

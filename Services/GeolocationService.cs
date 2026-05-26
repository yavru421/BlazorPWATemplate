using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class GeolocationService : IGeolocationService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public GeolocationService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/geo.module.js").AsTask());
    }

    public async Task<GeolocationResult> GetCurrentPositionAsync()
    {
        try
        {
            var module = await moduleTask.Value;
            var raw = await module.InvokeAsync<RawGeolocationCoords>("getCurrentPosition");
            return new GeolocationResult
            {
                Success = true,
                Latitude = raw.Latitude,
                Longitude = raw.Longitude,
                Accuracy = raw.Accuracy
            };
        }
        catch (JSException ex)
        {
            return new GeolocationResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
        catch (Exception ex)
        {
            return new GeolocationResult
            {
                Success = false,
                ErrorMessage = $"Unknown error: {ex.Message}"
            };
        }
    }

    public async Task<int> WatchPositionAsync<T>(DotNetObjectReference<T> dotNetHelper, string callbackMethodName) where T : class
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<int>("watchPosition", dotNetHelper, callbackMethodName);
    }

    public async Task ClearWatchAsync(int watchId)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("clearWatch", watchId);
    }

    public async Task LoadLeafletAsync()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("loadLeaflet");
    }

    public async Task InitMapAsync(string elementId, double lat, double lng)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("initMap", elementId, lat, lng);
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }

    private class RawGeolocationCoords
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Accuracy { get; set; }
    }
}

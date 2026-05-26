namespace BlazorPwaTemplate.Services;

public interface IHapticsService : IAsyncDisposable
{
    Task<bool> IsSupportedAsync();
    Task VibrateAsync(int durationMs);
    Task VibratePatternAsync(int[] pattern);
}

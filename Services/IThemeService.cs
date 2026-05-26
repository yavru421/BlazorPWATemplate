using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public interface IThemeService : IAsyncDisposable
{
    Task<string> GetSystemThemeAsync();
    Task WatchSystemThemeAsync<T>(DotNetObjectReference<T> dotNetHelper, string callbackMethodName) where T : class;
    Task ApplyThemeAsync(string theme);
}

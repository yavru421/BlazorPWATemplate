using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class ThemeService : IThemeService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public ThemeService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/theme.module.js").AsTask());
    }

    public async Task<string> GetSystemThemeAsync()
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<string>("getSystemTheme");
    }

    public async Task WatchSystemThemeAsync<T>(DotNetObjectReference<T> dotNetHelper, string callbackMethodName) where T : class
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("watchSystemTheme", dotNetHelper, callbackMethodName);
    }

    public async Task ApplyThemeAsync(string theme)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("applyTheme", theme);
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

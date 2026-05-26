using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class LocalStorageService : ILocalStorageService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/localStorage.module.js").AsTask());
    }

    public async Task<string?> GetItemAsync(string key)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<string?>("getItem", key);
    }

    public async Task SetItemAsync(string key, string value)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("setItem", key, value);
    }

    public async Task RemoveItemAsync(string key)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("removeItem", key);
    }

    public async Task ClearAsync()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("clear");
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

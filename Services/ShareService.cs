using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class ShareService : IShareService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public ShareService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/share.module.js").AsTask());
    }

    public async Task<bool> IsSupportedAsync()
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("isSupported");
    }

    public async Task<bool> ShareAsync(string title, string text, string url)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("share", title, text, url);
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

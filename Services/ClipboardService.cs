using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class ClipboardService : IClipboardService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public ClipboardService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/clipboard.module.js").AsTask());
    }

    public async Task<bool> CopyTextAsync(string text)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("copyText", text);
    }

    public async Task<bool> WriteTextAsync(string text)
    {
        return await CopyTextAsync(text);
    }

    public async Task<string> ReadTextAsync()
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<string>("readText");
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

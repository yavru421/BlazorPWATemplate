using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class BluetoothService : IBluetoothService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public BluetoothService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/bluetooth.module.js").AsTask());
    }

    public async Task<string> RequestDeviceAsync()
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<string>("requestDevice");
    }

    public async Task<bool> ScanNfcAsync<T>(DotNetObjectReference<T> dotNetHelper, string callbackMethodName) where T : class
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("scanNfc", dotNetHelper, callbackMethodName);
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

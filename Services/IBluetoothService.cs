using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class NfcScanResult
{
    public string SerialNumber { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}

public interface IBluetoothService : IAsyncDisposable
{
    Task<string> RequestDeviceAsync();
    Task<bool> ScanNfcAsync<T>(DotNetObjectReference<T> dotNetHelper, string callbackMethodName) where T : class;
}

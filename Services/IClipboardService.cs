namespace BlazorPwaTemplate.Services;

public interface IClipboardService : IAsyncDisposable
{
    Task<bool> CopyTextAsync(string text);
    Task<bool> WriteTextAsync(string text);
    Task<string> ReadTextAsync();
}

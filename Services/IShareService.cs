namespace BlazorPwaTemplate.Services;

public interface IShareService : IAsyncDisposable
{
    Task<bool> IsSupportedAsync();
    Task<bool> ShareAsync(string title, string text, string url);
}

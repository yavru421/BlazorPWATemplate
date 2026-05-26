namespace BlazorPwaTemplate.Services;

public interface ILocalStorageService : IAsyncDisposable
{
    Task<string?> GetItemAsync(string key);
    Task SetItemAsync(string key, string value);
    Task RemoveItemAsync(string key);
    Task ClearAsync();
}

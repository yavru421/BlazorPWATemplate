namespace BlazorPwaTemplate.Services;

public interface IPwaInstallService : IAsyncDisposable
{
    event Action? OnInstallableChanged;
    bool IsInstallable { get; }
    Task PromptInstallAsync();
}

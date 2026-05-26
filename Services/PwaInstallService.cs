using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class PwaInstallService : IPwaInstallService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;
    private DotNetObjectReference<PwaInstallService>? dotNetHelper;
    private bool isInstallable;

    public event Action? OnInstallableChanged;

    public bool IsInstallable => isInstallable;

    public PwaInstallService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/pwa.module.js").AsTask());

        // Initialize helper and attach callbacks to JS PWA module
        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        try
        {
            var module = await moduleTask.Value;
            dotNetHelper = DotNetObjectReference.Create(this);
            await module.InvokeVoidAsync("onInstall", dotNetHelper, nameof(OnInstallableChangedCallback));
            isInstallable = await module.InvokeAsync<bool>("isAvailable");
            OnInstallableChanged?.Invoke();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error initializing PwaInstallService: {ex.Message}");
        }
    }

    [JSInvokable]
    public void OnInstallableChangedCallback(bool installable)
    {
        isInstallable = installable;
        OnInstallableChanged?.Invoke();
    }

    public async Task PromptInstallAsync()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("prompt");
    }

    public async ValueTask DisposeAsync()
    {
        dotNetHelper?.Dispose();
        if (moduleTask.IsValueCreated)
        {
            try
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error disposing PwaInstallService JS module: {ex.Message}");
            }
        }
    }
}

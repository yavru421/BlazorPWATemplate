namespace BlazorPwaTemplate.Services;

public enum AppTheme { System, Dark, Light }

public sealed class AppStateContainer
{
    public AppTheme Theme { get; private set; } = AppTheme.Dark;
    public bool SidebarCollapsed { get; private set; }
    public string? LastVisitedRoute { get; private set; }
    
    public event Action? OnChange;

    public void SetTheme(AppTheme t) { Theme = t; NotifyStateChanged(); }
    public void SetSidebarCollapsed(bool v) { SidebarCollapsed = v; NotifyStateChanged(); }
    public void SetLastVisitedRoute(string route) { LastVisitedRoute = route; NotifyStateChanged(); }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

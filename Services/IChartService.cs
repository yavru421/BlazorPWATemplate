namespace BlazorPwaTemplate.Services;

public interface IChartService : IAsyncDisposable
{
    Task DrawLineChartAsync(string canvasId, double[] data, string[] labels, object? options = null);
    Task DrawBarChartAsync(string canvasId, double[] data, string[] labels, object? options = null);
    Task DrawDonutChartAsync(string canvasId, double[] data, string[] labels, object? options = null);
}

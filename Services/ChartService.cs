using Microsoft.JSInterop;

namespace BlazorPwaTemplate.Services;

public class ChartService : IChartService
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public ChartService(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/chart.module.js").AsTask());
    }

    public async Task DrawLineChartAsync(string canvasId, double[] data, string[] labels, object? options = null)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("drawLineChart", canvasId, data, labels, options);
    }

    public async Task DrawBarChartAsync(string canvasId, double[] data, string[] labels, object? options = null)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("drawBarChart", canvasId, data, labels, options);
    }

    public async Task DrawDonutChartAsync(string canvasId, double[] data, string[] labels, object? options = null)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("drawDonutChart", canvasId, data, labels, options);
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

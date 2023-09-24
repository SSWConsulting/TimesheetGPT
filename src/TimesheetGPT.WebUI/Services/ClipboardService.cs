// https://www.meziantou.net/copying-text-to-clipboard-in-a-blazor-application.htm
namespace TimesheetGPT.WebUI.Services;

using System.Threading.Tasks;
using Microsoft.JSInterop;

public sealed class ClipboardService(IJSRuntime jsRuntime)
{
    public ValueTask<string> ReadTextAsync()
    {
        return jsRuntime.InvokeAsync<string>("navigator.clipboard.readText");
    }

    public ValueTask WriteTextAsync(string text)
    {
        return jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }
}
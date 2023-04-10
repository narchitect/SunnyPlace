using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SunnyPlace_ASP.NETCore.Pages;

public class IndexModel : PageModel
{
    public string Message { get; private set; } = "PageModel in C#";
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        Message += $"Sever time is {DateTime.Now}";
    }
}


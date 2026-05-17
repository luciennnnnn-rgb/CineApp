using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CineApp.Pages;

public class ErrorModel : PageModel
{
    public int StatusCode { get; set; }

    public void OnGet(int statusCode)
    {
        StatusCode = statusCode;
    }
}
using System.Net;
using Auth0.AspNetCore.Authentication;
using IntelliHouse2000.Models;
using IntelliHouse2000.Services.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IntelliHouse2000.Pages;

public class Logout : PageModel
{
    private IDBService _dbService;

    public Logout(IDBService dbService)
    {
        _dbService = dbService;
    }

    public async Task OnGet()
    {
        var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri("/")
            .Build();
        
        var debug = _dbService.WriteLogAsync(new LogMessage
        {
            Client = HttpContext.User.Identity.Name,
            Message = $"User logged out",
            Timestamp = DateTime.Now,
            Topic = "home/log/user"
        });
        await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        
    }
}
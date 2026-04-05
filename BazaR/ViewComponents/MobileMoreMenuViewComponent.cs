using BazaR.Data;
using BazaR.Models;
using BazaR.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BazaR.ViewComponents;

public class MobileMoreMenuViewComponent : ViewComponent
{
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _db;

    public MobileMoreMenuViewComponent(UserManager<User> userManager, AppDbContext db)
    {
        _userManager = userManager;
        _db = db;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (User.Identity?.IsAuthenticated != true)
            return View("Guest");

        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null)
            return View("Guest");

        var unread = await _db.Messages
            .AsNoTracking()
            .CountAsync(m => m.UserId == user.Id && !m.IsRead);

        var first = user.FirstName?.Trim() ?? "";
        var last = user.LastName?.Trim() ?? "";
        var display = $"{first} {last}".Trim();
        if (string.IsNullOrEmpty(display))
            display = user.Email ?? user.UserName ?? "Профіль";

        return View("Default", new MobileMoreMenuViewModel
        {
            DisplayName = display,
            UnreadMessages = unread
        });
    }
}
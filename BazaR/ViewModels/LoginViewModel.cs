using Microsoft.AspNetCore.Authentication;

namespace BazaR.ViewModels
{
    public class LoginViewModel
    {
        public string? ReturnUrl { get; set; }
        public List<AuthenticationScheme> ExternalLogins { get; set; } = new();
    }
}

using System.ComponentModel.DataAnnotations;

namespace BazaR.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Пароль і пароль підтвердження не збігаються.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}

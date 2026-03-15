namespace BazaR.ViewModels
{
    public class AccountProfileViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public int NewMessagesCount { get; set; }

        public string FullName => string.IsNullOrWhiteSpace(FirstName) ? Email : FirstName;
    }
}
using BazaR.Models;

namespace BazaR.ViewModels
{
    public class AccountProfileViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public int NewMessagesCount { get; set; }

        public List<OrderRecipient> OrderRecipients { get; set; } = new();
        public List<DeliveryAddress> DeliveryAddresses { get; set; } = new();
        public List<Pet> Pets { get; set; } = new();
        public List<AdditionalInfo> AdditionalInfos { get; set; } = new();

        public string FullName =>
            string.IsNullOrWhiteSpace(FirstName) ? Email : FirstName;
    }
}
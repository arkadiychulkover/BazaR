namespace BazaR.ViewModels
{
    public class MailingViewModel
    {
        public AccountProfileViewModel Profile { get; set; }

        public bool NewsAndUpdates { get; set; }

        public bool SpecialOffers { get; set; }

        public bool PersonalRecommendations { get; set; }

        public bool ProductAlerts { get; set; }

        public bool WeeklyDigest { get; set; }

        public string PreferredFrequency { get; set; }
    }
}

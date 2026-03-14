namespace BazaR.ViewModels
{
    public class PremiumViewModel
    {
        public AccountProfileViewModel Profile { get; set; }

        public bool IsActive { get; set; }

        public string PlanType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal MonthlyPrice { get; set; }

        public bool AutoRenewal { get; set; }

        public List<PremiumFeatureViewModel> Features { get; set; } = new();
    }

    public class PremiumFeatureViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsAvailable { get; set; }
    }
}

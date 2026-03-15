namespace BazaR.ViewModels
{
    public class BonusAccountViewModel
    {
        public AccountProfileViewModel Profile { get; set; }

        public int TotalBalance { get; set; }

        public int MonthlyAccrued { get; set; }

        public int MonthlySpent { get; set; }

        public int AccrualRate { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}

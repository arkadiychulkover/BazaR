using BazaR.Models;

namespace BazaR.ViewModels
{
    public class WalletViewModel
    {
        public AccountProfileViewModel Profile { get; set; }
        public decimal Balance { get; set; }
        public decimal MonthlySpent { get; set; }
        public decimal? LastReplenishmentAmount { get; set; }
        public DateTime? LastReplenishmentDate { get; set; }
        public string Status { get; set; } = "Активний гаманець";
        public List<WalletTransaction> Transactions { get; set; } = new();
    }
}

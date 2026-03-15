namespace BazaR.ViewModels
{
    public class ReviewVm
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string ItemImageUrl { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class AccountReviewsViewModel
    {
        public AccountProfileViewModel Profile { get; set; } = new();
        public List<ReviewVm> Reviews { get; set; } = new();
    }
}
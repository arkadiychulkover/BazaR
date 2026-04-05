using BazaR.Models;

namespace BazaR.ViewModels
{
    public class MessageVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public bool IsRead { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
    }

    public class AccountMessagesViewModel
    {
        public AccountProfileViewModel Profile { get; set; }
        public List<MessageVm> Messages { get; set; } = new();
        public int NewMessagesCount { get; set; }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public int StartPage
        {
            get
            {
                var start = CurrentPage - 2;
                if (start < 1)
                    start = 1;

                var end = start + 4;
                if (end > TotalPages)
                {
                    end = TotalPages;
                    start = Math.Max(1, end - 4);
                }

                return start;
            }
        }

        public int EndPage
        {
            get
            {
                var end = StartPage + 4;
                return end > TotalPages ? TotalPages : end;
            }
        }

        public int PrevJumpPage => Math.Max(1, CurrentPage - 1);
        public int NextJumpPage => Math.Min(TotalPages, CurrentPage + 1);
    }
}
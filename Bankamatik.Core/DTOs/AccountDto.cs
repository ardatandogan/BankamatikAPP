namespace Bankamatik.Core.DTOs
{
    public class AccountDTO
    {
        public int AccountID { get; set; }
        public int? UserID { get; set; }
        public decimal? Balance { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

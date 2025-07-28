namespace Bankamatik.Core.Entities
{
    public class Account
    {
        public int AccountID { get; set; }
        public int? UserID { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ParaCinsi { get; set; } = "TRY";
        public string AccountDisplay
        {
            get
            {
                return $"ID: {AccountID} | {ParaCinsi} | Balance: {Balance}₺";
            }
        }
    }
}

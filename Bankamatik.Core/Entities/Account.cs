namespace Bankamatik.Core.Entities
{
    public class Account
    {
        public int AccountID { get; set; }
        public int UserID { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

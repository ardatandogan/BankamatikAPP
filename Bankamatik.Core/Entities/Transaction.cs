namespace Bankamatik.Core.Entities
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int FromAccountID { get; set; }
        public int ToAccountID { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}

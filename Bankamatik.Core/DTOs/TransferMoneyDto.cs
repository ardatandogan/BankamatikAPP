namespace Bankamatik.Core.DTOs
{
    public class TransferMoneyDTO
    {
        public int FromAccountID { get; set; }
        public int ToAccountID { get; set; }
        public decimal Amount { get; set; }
    }
}

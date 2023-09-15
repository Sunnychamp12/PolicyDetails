namespace PolicyDetails.Models.Account
{
    public class AccountTransactionDetails
    {
        public int TransactionId { get; set; }
        public Nullable<System.DateTime> TransDate { get; set; }
        public string Details { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public Nullable<decimal> Balance { get; set; }
    }
}

namespace PolicyDetails.Models
{
    public class PolicyTransactionRequest
    {
        public int PolicyNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

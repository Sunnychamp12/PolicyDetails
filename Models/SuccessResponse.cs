namespace PolicyDetails.Models
{
    public class SuccessResponse
    {
        public string Status { get; set; }
        public List<PolicyData> PolicyDetails { get; set; }
    }
    public class GetSuccessResponse
    {
        public int Status { get; set; }
        public List<PolicyData> PolicyDetails { get; set; }
    }
    public class GetPolicyTransactionResponse
    {
        public int Status { get; set; }
        public List<CustomerDetails> customerDetails { get; set; }
    }
}

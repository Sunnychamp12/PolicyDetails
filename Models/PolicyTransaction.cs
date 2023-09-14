namespace PolicyDetails.Models
{
    public class PolicyTransaction
    {
        public int PolicyNo { get; set; }
        public int CustomerId { get; set; }
        public decimal? PremiumAmount { get; set; }
        public DateTime? PremiumDate { get; set; }
        public string PolicyStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditDate { get; set; }
        public int PolicyKey { get; set; }
        public int ContractNumber { get; set; }
        public string CustomerCode { get; set; }
        public DateTime? RiskCommencementDate { get; set; }
        public string ProductName { get; set; }
        public DateTime? MaturityDate { get; set; }
        public DateTime? NextRenewalDue { get; set; }
        public decimal SumAssuredAmount { get; set; }
        public decimal PDAmt { get; set; }
        public decimal ContractStatusCode { get; set; }
        public string PD_Status { get; set; }
        public DateTime? ETLDate { get; set; }
        public long? dataByC_ID { get; set; }
    }
}

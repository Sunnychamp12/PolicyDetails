namespace PolicyDetails.Models
{
    public class SuccessResponse
    {
        public string Status { get; set; }
        public List<PolicyData> PolicyDetails { get; set; }
    }
}

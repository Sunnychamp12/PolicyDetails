using System.ComponentModel.DataAnnotations;

namespace PolicyDetails.Models.Account
{
    public class AccountTransactionRequest
    {
        [Required]
        public int AccountNo { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationIdentity.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PaymentId { get; set; }
       
        public string EventDetailId { get; set; }

        [ForeignKey("EventDetailId")]
        public EventDetail EventDetail { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}

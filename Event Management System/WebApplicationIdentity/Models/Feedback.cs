using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationIdentity.Models
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string FeedbackId { get; set; }
        public string EventDetailId { get; set; }

        [ForeignKey("EventDetailId")]
        public EventDetail EventDetail { get; set; }
        public string Comments { get; set; }
        public DateTime DateSubmitted { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}

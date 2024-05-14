using System.ComponentModel.DataAnnotations;

namespace VOSA_Events.Models
{
    public class Review
    {
        public int AccountID { get; set; }
        public virtual Account Account { get; set; }

        public int EventID { get; set; }
        public virtual Event Event { get; set; }


        [MaxLength(300, ErrorMessage = "Recensionen får vara max 300 tecken.")]
        public string? Description { get; set; }

        public int? Rating { get; set; }
    }
}

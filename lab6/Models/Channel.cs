using System.ComponentModel.DataAnnotations;

namespace lab6.Models
{
    public class Channel
    {
        [Key]
        public int Channel_ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Channel_Name { get; set; }

        [StringLength(255)]
        public string Other_Details { get; set; }

        // Navigation property
        public virtual ICollection<Event> Events { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace lab6.Models
{
    public class Customer
    {
        [Key]
        public int Customer_ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Customer_Name { get; set; }

        [StringLength(15)]
        public string Title { get; set; }

        [StringLength(1)]
        public string Gender_MFU { get; set; }

        [StringLength(255)]
        public string Other_Details { get; set; }

        // Navigation property
        public virtual ICollection<Event> Events { get; set; }
    }
}

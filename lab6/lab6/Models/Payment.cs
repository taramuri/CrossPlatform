using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lab6.Models
{
    public class Payment
    {
        [Key]
        public int Payment_ID { get; set; }

        public int Event_ID { get; set; }

        public DateTime Payment_Date { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Payment_Amount { get; set; }

        [StringLength(255)]
        public string Other_Details { get; set; }

        // Navigation property
        public virtual Event Event { get; set; }
    }
}

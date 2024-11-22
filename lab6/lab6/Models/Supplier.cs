using System.ComponentModel.DataAnnotations;

namespace lab6.Models
{
    public class Supplier
    {
        [Key]
        public int Supplier_ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Supplier_Name { get; set; }

        [StringLength(255)]
        public string Other_Details { get; set; }

        // Navigation property
        public virtual ICollection<ProductService> Products { get; set; }
    }
}

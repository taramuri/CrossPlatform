using System.ComponentModel.DataAnnotations;

namespace lab6.Models
{
    public class ProductService
    {
        [Key]
        [StringLength(15)]
        public string Prod_Service_Code { get; set; }

        public int Supplier_ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Prod_Service_Name { get; set; }

        [StringLength(255)]
        public string Other_Details { get; set; }

        // Navigation property
        public virtual Supplier Supplier { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace lab6.Models
{
    public class CustomerDataPlatform
    {
        [Key]
        [StringLength(15)]
        public string Platform_Code { get; set; }

        [Required]
        [StringLength(255)]
        public string Customer_Name { get; set; }

        [StringLength(255)]
        public string Customer_Platform_Details { get; set; }
    }
}

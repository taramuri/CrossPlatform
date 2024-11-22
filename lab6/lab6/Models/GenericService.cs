using System.ComponentModel.DataAnnotations;

namespace lab6.Models
{
    public class GenericService
    {
        [Key]
        [StringLength(15)]
        public string Service_Code { get; set; }

        [Required]
        [StringLength(255)]
        public string Service_Name { get; set; }

        [StringLength(255)]
        public string Other_Details { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace lab6.Models
{
    public class Platform
    {
        [Key]
        [StringLength(15)]
        public string Platform_Code { get; set; }

        [Required]
        [StringLength(255)]
        public string Platform_Name { get; set; }

        [StringLength(255)]
        public string Other_Details { get; set; }

        [StringLength(1)]
        public string Asset_Mgt { get; set; }

        [StringLength(1)]
        public string Hotel { get; set; }
    }
}

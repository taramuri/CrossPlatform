using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace lab6.Models
{
    public class RefDocumentType
    {
        [Key]
        [StringLength(15)]
        public string Document_Type_Code { get; set; }

        [Required]
        [StringLength(255)]
        public string Document_Type_Description { get; set; }

        [StringLength(1)]
        public string Document_Type_Category { get; set; }

        // Navigation property
        public virtual ICollection<Document> Documents { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace lab6.Models
{
    public class Document
    {
        [Key]
        public int Document_ID { get; set; }

        [Required]
        [StringLength(15)]
        public string Document_Type_Code { get; set; }

        public int Event_ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Document_Name { get; set; }

        [StringLength(255)]
        public string Document_Text { get; set; }

        [StringLength(255)]
        public string Other_Details { get; set; }

        // Navigation properties
        public virtual Event Event { get; set; }
        public virtual RefDocumentType DocumentType { get; set; }
    }
}

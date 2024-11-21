﻿using System.ComponentModel.DataAnnotations;

namespace lab6.Models
{
    public class Artefact
    {
        [Key]
        public int Artefact_ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Artefact_Name { get; set; }

        [StringLength(255)]
        public string Other_Details { get; set; }

        // Navigation property
        public virtual ICollection<Event> Events { get; set; }
    }
}

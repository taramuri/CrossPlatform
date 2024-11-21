using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Threading.Channels;

namespace lab6.Models
{
    public class Event
    {
        [Key]
        public int Event_ID { get; set; }

        public int? Artefact_ID { get; set; }
        public int Channel_ID { get; set; }
        public int Customer_ID { get; set; }
        public int? Event_Sequence_ID { get; set; }
        public int Location_ID { get; set; }

        [StringLength(15)]
        public string Platform_Code { get; set; }

        [StringLength(15)]
        public string Service_Code { get; set; }

        public int Staff_ID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Event_Amount { get; set; }

        public DateTime Event_Date_Time { get; set; }
        public DateTime Booking_Date_From { get; set; }
        public DateTime Booking_Date_To { get; set; }

        [StringLength(255)]
        public string Other_Details { get; set; }

        // Navigation properties
        public virtual Customer Customer { get; set; }
        public virtual Channel Channel { get; set; }
        public virtual Location Location { get; set; }
        public virtual EventSequence EventSequence { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual Artefact Artefact { get; set; }
    }
}

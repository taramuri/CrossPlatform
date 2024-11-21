using System.ComponentModel.DataAnnotations;

namespace lab6.Models
{
    public class EventSequence
    {
        [Key]
        public int Event_Sequence_ID { get; set; }

        public int? Next_Event_Sequence_ID { get; set; }

        [StringLength(15)]
        public string Event_Code { get; set; }

        public DateTime Event_Date_Time { get; set; }

        [StringLength(255)]
        public string Other_Details { get; set; }

        [StringLength(1)]
        public string Book_Hotel { get; set; }

        [StringLength(1)]
        public string Check_Out_Pay { get; set; }

        // Navigation properties
        public virtual EventSequence NextSequence { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}

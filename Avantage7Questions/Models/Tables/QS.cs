using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avantage7Questions.Models.Tables
{
    public class QS
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int QuestionId { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}

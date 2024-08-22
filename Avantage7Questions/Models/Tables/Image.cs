using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avantage7Questions.Models.Tables
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FilePath { get; set; }
        public int QuestionId {  get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

    }
}

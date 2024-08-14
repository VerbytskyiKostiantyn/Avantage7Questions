using System.ComponentModel.DataAnnotations;

namespace Avantage7Questions.Models.Tables
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string BackgroundColor { get; set; }
    }
}

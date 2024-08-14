using System.ComponentModel.DataAnnotations;

namespace Avantage7Questions.Models.Tables
{
    public class Items
    {
        [Key]
        public int Id { get; set; }
        public string? Computer { get; set; }
        public string? Laptop { get; set; }
        public string? Mouse { get; set; }
        public string? Keyboard { get; set; }
        public string? Monitor { get; set; }
        public string? Phone { get; set; }
        public string? Another { get; set; }
        public string? AnotherText { get; set; }

    }
}

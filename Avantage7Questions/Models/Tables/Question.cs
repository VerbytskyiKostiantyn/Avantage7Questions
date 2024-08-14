using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avantage7Questions.Models.Tables
{
	public class Question
	{
		[Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Потрібно заповнити це поле")]
        [StringLength(50)]
		public string Name { get; set; }
        [Required(ErrorMessage = "Потрібно заповнити це поле")]
        [StringLength(50)]
		public string LastName { get; set; }
        [Required(ErrorMessage = "Потрібно заповнити це поле")]
		[MinLength(17, ErrorMessage = "Введіть правильний номер телефону")]
		public string PhoneNumber { get; set; }
		[StringLength(5000)]
		public string? Data { get; set; }
        [ForeignKey("Items")]
        public int ItemsId { get; set; }
        public Items Items { get; set; }
        [ForeignKey("QuestionId")]
        public ICollection<QS> QS { get; set; }

        [NotMapped]
        public string ItemsString { get; set; }
        [NotMapped]
        public string CurrentStatus { get; set; }
        [NotMapped]
        public string LastUpdateDate { get; set; }
        [NotMapped]
        public string CreateQuestionDate { get; set; }
        [NotMapped]
        public bool IsPhotos { get; set; }
        [NotMapped]
        public string StatusBackgroundColor { get; set; }
	}
}

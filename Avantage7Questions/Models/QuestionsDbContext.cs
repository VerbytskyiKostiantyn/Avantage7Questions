using Avantage7Questions.Models.Tables;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Xml;

namespace Avantage7Questions.Models
{
	public class QuestionsDbContext: IdentityDbContext<IdentityUser>
    {
		public QuestionsDbContext(DbContextOptions<QuestionsDbContext> options) : base(options)
		{

		}

		public DbSet<Question> Questions	{ get; set; }
		public DbSet<Image> Images { get; set; }
        public DbSet<Items> Itemses { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<QS> QSes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Status>().HasData(
                new Status {Id = 1, Name = "New", Description ="Нове", BackgroundColor = "#34c0eb" },
                new Status { Id = 2, Name = "Seen", Description = "Переглянуто", BackgroundColor = "#1d4ed8" },
                new Status { Id = 3, Name = "Confirmed", Description = "Підтверджено", BackgroundColor = "#bfb115" },
                new Status { Id = 4, Name = "Ordered", Description = "Замовлено", BackgroundColor = "#d4872f" },
                new Status { Id = 5, Name = "Delivered", Description = "Доставлено", BackgroundColor = "#1c994e" },
                new Status { Id = 6, Name = "Finished", Description = "Завершено", BackgroundColor = "#727372" },
                new Status { Id = 7, Name = "Canceled", Description = "Відмінено", BackgroundColor = "#b91c1c" }
                );
        }
    }
}

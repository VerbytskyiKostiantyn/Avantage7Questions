using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Avantage7Questions.Models
{
    public class DbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly QuestionsDbContext _db;

        public DbInitializer(UserManager<IdentityUser> userManager, QuestionsDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public void InitializeDb()
        {

            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }

            if (!(_userManager.Users.Count() > 0))
            {
                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "admin@avantage7.com.ua",
                    Email = "admin@avantage7.com.ua",
                    EmailConfirmed = true
                }, "Admin123!");
                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "kosver@avantage7.com.ua",
                    Email = "kosver@avantage7.com.ua",
                    EmailConfirmed = true
                }, "Admin123!");
                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "popovl@avantage7.com.ua",
                    Email = "popovl@avantage7.com.ua",
                    EmailConfirmed = true
                }, "Admin123!");
                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "abilyk@avantage7.com.ua",
                    Email = "abilyk@avantage7.com.ua",
                    EmailConfirmed = true
                }, "Admin123!");
                _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "kirichenko@avantage7.com.ua",
                    Email = "kirichenko@avantage7.com.ua",
                    EmailConfirmed = true
                }, "Admin123!");

            }
        }
    }
}

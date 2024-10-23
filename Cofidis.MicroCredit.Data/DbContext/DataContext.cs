using Cofidis.MicroCredit.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cofidis.MicroCredit.Data
{
    public class DataContext : DbContext
    {        //DB Set´s
        public DbSet<GrantingMicroCredit> grantingMicroCredits { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
            // Create database and apply migrations
            Database.EnsureCreated();

        }

    }
}

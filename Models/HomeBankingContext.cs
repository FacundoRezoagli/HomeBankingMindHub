using Microsoft.EntityFrameworkCore;

namespace HomeBankingMindHub.Models
{
    public class HomeBankingContext : DbContext
    {
        public HomeBankingContext(DbContextOptions<HomeBankingContext> options): base(options)
        { 
            
        }

        //dbsets
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Account { get; set; }
    }
}

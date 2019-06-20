using Microsoft.EntityFrameworkCore;

namespace crm.Models
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> option): base(option){}
        protected override void OnModelCreating(ModelBuilder builder){}
        public DbSet<Customer> customers {get; set;}
        public DbSet<Payment> payments {get; set;}
        public DbSet<TokenAccess> TokenAccesss {get; set;}
    }
}
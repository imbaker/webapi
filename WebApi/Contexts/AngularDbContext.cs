using Microsoft.EntityFrameworkCore;

namespace WebApi.Contexts
{
    public class AngularDbContext : DbContext
    {
        public DbSet<Page> Pages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\MsSqlLocalDb;Database=AngularDB");
        }
    }

    public class Page
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
    }
}
using Core.Domains.Languages;
using Core.Page;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Data.Context
{
    public class AppDbContext : IdentityDbContext
    {
        private readonly string _connectionString;
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions,IOptions<ConnectionStrings> options):base(dbContextOptions)
        {
            _connectionString=options.Value.DefaultConnection;
            
        }

       
        // Names of Tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Language>().ToTable("Languages");
            modelBuilder.Entity<HomePage>().ToTable("HomePages");
            modelBuilder.Entity<NavBarItem>().ToTable("NavBarItems");
            modelBuilder.Entity<NavBarItemSubItem>().ToTable("NavBarItemSubItems");
        }

        // Tables
        public DbSet<Language> Languages { get; set; }
        public DbSet<HomePage> HomePages { get; set; }
        public DbSet<NavBarItem> NavBarItems { get; set; }
        public DbSet<NavBarItemSubItem> NavBarItemSubItems { get; set; }
    }
}

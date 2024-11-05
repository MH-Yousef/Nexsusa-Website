using Core.Domains.Languages;
using Core.Page;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AppDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=45.84.189.34\\MSSQLSERVER2022;Database=ragnarzz_Nexsusa;User Id=ragnarzz_sa;Password=&80wtJ3u8;TrustServerCertificate=True;");
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

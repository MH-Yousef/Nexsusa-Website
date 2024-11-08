using Core.Domains.Languages;
using Core.HomePage;
using Core.HomePage.ChooseUs;
using Core.HomePage.ClientSays;
using Core.HomePage.NavBar;
using Core.HomePage.OurCompany;
using Core.HomePage.OurEmployees;
using Core.HomePage.RegularBlogs;
using Core.HomePage.Services;
using Core.HomePage.Slider;
using Core.HomePage.WhoWeAre;
using Core.HomePage.WorkingProcess;
using Core.HomePage.WorkShowCase;
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
        public DbSet<ChooseUs> ChooseUs { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ClientSays> ClientSays { get; set; }
        public DbSet<ClientSaysItem> ClientSaysItems { get; set; }
        public DbSet<OurCompany> OurCompanys { get; set; }
        public DbSet<OurEmployees> OurEmployees { get; set; }
        public DbSet<OurEmployeesItem> OurEmployeesItems { get; set; }
        public DbSet<RegularBlogs> RegularBlogs { get; set; }
        public DbSet<RegularBlogsItem> RegularBlogsItems { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<WhoWeAre> WhoWeAres { get; set; }
        public DbSet<WhoWeAreItem> WhoWeAreItems { get; set; }
        public DbSet<WorkingProcess> WorkingProcesses { get; set; }
        public DbSet<WorkingProcessItem> WorkingProcessItems { get; set; }
        public DbSet<WorkShowCase> WorkShowCases { get; set; }
        public DbSet<WorkShowCaseNavBar> WorkShowCaseNavBars { get; set; }
        public DbSet<WorkShowCaseNavBarItem> WorkShowCaseNavBarItems { get; set; }
        public DbSet<WorkShowCaseService> WorkShowCaseServices { get; set; }
    }
}

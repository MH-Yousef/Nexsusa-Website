using Core.AboutPage;
using Core.ContactUsPage;
using Core.Domains.Languages;
using Core.HomePage;
using Core.HomePage.HomePageItems;
using Core.ServicesPage;
using Core.SocialLinks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Data.Context
{
    public class AppDbContext : DbContext
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
        }

        // Tables
        public DbSet<Language> Languages { get; set; }
        public DbSet<StringResource> StringResources { get; set; }
        public DbSet<Translate> Translates { get; set; }



        public DbSet<HomePage> HomePages { get; set; }
        public DbSet<HomePageInfo> HomePageInfos { get; set; }
        public DbSet<SocialLink> SocialLinks { get; set; }

        public DbSet<About> Abouts { get; set; }
        public DbSet<ServicePage> ServicePages { get; set; }



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
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<QuickLink> QuickLinks { get; set; }
        public DbSet<WhoWeAre> WhoWeAres { get; set; }
        public DbSet<WhoWeAreItem> WhoWeAreItems { get; set; }
        public DbSet<WorkingProcess> WorkingProcesses { get; set; }
        public DbSet<WorkingProcessItem> WorkingProcessItems { get; set; }
        public DbSet<WorkShowCase> WorkShowCases { get; set; }
        public DbSet<ContactUs> ContactUss { get; set; }
    }
}

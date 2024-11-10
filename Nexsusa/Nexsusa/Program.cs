using Core.Domains.Languages;
using Core.HomePage.HomePageItems;
using Core.ServicesPage;
using Data;
using Data.Dtos.NavBarDTOs;
using Data.Mapping;
using Microsoft.AspNetCore.Localization;
using Services._ConfigureServices;
using Services.HomePageServices.ChooseUsServices;
using Services.HomePageServices.NavBarItemServices;
using Services.LanguageServices;
using System.Drawing;
using System.Globalization;
using System.Security.Policy;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Dependencies(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add Interfaces and Services
builder.Services.ConfigureApplicationServices();

// Add AutoMapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// AllowAllOrigins
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var serviceProvider = builder.Services.BuildServiceProvider();

#region Add En Language

var languageService = serviceProvider.GetRequiredService<ILanguageService>();
var languages = await languageService.Get();
if (!languages.Any())
{
    var language = new Language
    {
        Shortcut = "en",
        CreatedDate = DateTime.Now,
        IsDeleted = false,
        StringResources = new List<StringResource>(),
        UpdatedDate = DateTime.Now,
        Culture = "en-US",
        Name = "English",
        IsDefault = true,
        IsActive = true,
        IsRtl = false,
    };
    await languageService.Create(language);
}
var cultures = languages?.Select(x => new CultureInfo(x.Culture)).ToArray();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var englishCulture = cultures.FirstOrDefault(x => x.Name == "en-US");
    options.DefaultRequestCulture = new RequestCulture(englishCulture?.Name ?? "en-US");

    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

#endregion

//Test
//var NavbarService = serviceProvider.GetRequiredService<INavBarItemService>();
//var model = new List<NavBarItemDTO>
//{
//    new()
//    {
//    CreatedDate = DateTime.Now,
//    IsDeleted = false,
//    UpdatedDate = DateTime.Now,
//    Name = "Home",
//    Url = "/",
//    Icon = "fas fa-home",
//    LangId = 1,
//    IsVisible = true,
//    NavBarItemSubItems = null
//    },
//    new()
//    {
//    CreatedDate = DateTime.Now,
//    IsDeleted = false,
//    UpdatedDate = DateTime.Now,
//    Name = "Ana Sayfa",
//    Url = "/",
//    Icon = "fas fa-home",
//    LangId = 1,
//    IsVisible = true,
//    NavBarItemSubItems = null
//    }
//};
//await NavbarService.Create(model);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapRazorPages();
app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Dashboard}/{id?}");

app.MapControllers();
// Cors
app.UseCors();
app.Run();

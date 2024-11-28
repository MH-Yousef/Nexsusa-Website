using Core.Domains.Languages;
using Data;
using Data.Mapping;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using Services._ConfigureServices;
using Services.LanguageServices;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Dependencies(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddHttpContextAccessor();

// Add Interfaces and Services
builder.Services.ConfigureApplicationServices();


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

builder.Services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo { Title = "Nexsusa_Api", Version = "v1" }));

var serviceProvider = builder.Services.BuildServiceProvider();

#region Add En Language

var languageService = serviceProvider.GetRequiredService<ILanguageService>();
var languages = await languageService.Get();
if (languages.Data == null)
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
var cultures = languages.Data?.Select(x => new CultureInfo(x.Culture)).ToArray();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var englishCulture = cultures.FirstOrDefault(x => x.Name == "en-US");
    options.DefaultRequestCulture = new RequestCulture(englishCulture?.Name ?? "en-US");

    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

#endregion

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Nexsusa_Api"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();

// Cors
app.UseCors();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Dashboard}/{id?}");

app.MapControllers();

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Nexsusa_Api"));

app.Run();

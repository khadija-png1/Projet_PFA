using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Web_KB.Data;
using Web_KB.Repos;
using Web_KB.Repositories;
using Web_KB.Repositories.Interfaces;
using Web_KB.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC + TempData
builder.Services.AddControllersWithViews()
       .AddSessionStateTempDataProvider();

// Services
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IChangeMotDePasseService, ChangeMotDePasseService>();
builder.Services.AddScoped<ICandidatProfilService, CandidatProfilService>();
builder.Services.AddScoped<ICandidatRepos, CandidatRepos>();
builder.Services.AddScoped<IOffresEmploiService, OffresEmploiService>();
builder.Services.AddScoped<IOffresEmploiRepos, OffresEmploiRepos>();
builder.Services.AddScoped<IPublicationService, PublicationService>();
builder.Services.AddScoped<IPublicationRepos, PublicationRepos>();
builder.Services.AddScoped<ICandidatureService, CandidatureService>();
builder.Services.AddScoped<ICandidatureRepos, CandidatureRepos>();
builder.Services.AddScoped<IUtilisateurRepos, UtilisateursRepos>();

builder.Services.AddScoped<IRecruteurRepos, RecruteurRepos>();
builder.Services.AddScoped<IRectruteurProfilService, RectruteurProfilService>();
builder.Services.AddScoped<IEntrepriseRepos, EntrepriseRepos>();
builder.Services.AddScoped<IEntrepriseProfilService, EntrepriseProfilService>();

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Google OAuth
var googleSection = builder.Configuration.GetSection("Authentication:Google");
if (!string.IsNullOrEmpty(googleSection["ClientId"]) && !string.IsNullOrEmpty(googleSection["ClientSecret"]))
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/Compte/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.SameSite = SameSiteMode.Lax; // IMPORTANT pour localhost
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // localhost HTTPS ok
    })
    .AddGoogle(options =>
    {
        options.ClientId = googleSection["ClientId"]!;
        options.ClientSecret = googleSection["ClientSecret"]!;
        options.CallbackPath = "/signin-google";
        options.SaveTokens = true;
        options.CorrelationCookie.SameSite = SameSiteMode.Lax;
        options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });
}

// Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "WebKB.Session";
});

var app = builder.Build();

// Configuration pour le mode développement et gestion des erreurs
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Activation du routage et session
app.UseRouting();

// Activation de la session, authentification et autorisation
app.UseSession();  // La session doit être appelée avant UseAuthentication et UseAuthorization
app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePagesWithReExecute("/Error/{0}");

// Configuration de la route par défaut
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

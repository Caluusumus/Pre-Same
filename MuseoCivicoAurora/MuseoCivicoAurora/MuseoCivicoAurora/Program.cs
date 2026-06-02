using ClassModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MuseoCivicoAurora.Client.Pages;
using MuseoCivicoAurora.Client.Services;
using MuseoCivicoAurora.Components;
using MuseoCivicoAurora.Components.Account;
using MuseoCivicoAurora.Data;
using MuseoCivicoAurora.Endpoints;
using MuseoCivicoAurora.Service;
using Dapper;
using MuseoCivicoAurora.Helpers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddScoped<IArtworksService, ArtworksService>();
builder.Services.AddScoped<IBookingsService, BookingsService>();
builder.Services.AddScoped<IExhibitionsService, ExhibitionsService>();
builder.Services.AddScoped<ITicketsService, TicketsService>();
builder.Services.AddScoped<IToursService, ToursService>();

SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
SqlMapper.AddTypeHandler(new TimeOnlyTypeHandler());

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Assicurati di avere questo in cima

// Registra l'HttpClient in modo che il server possa chiamare le sue stesse API
builder.Services.AddHttpClient<ExhibitionApiClient>(c => c.BaseAddress = new Uri("https://localhost:7215"));
builder.Services.AddHttpClient<ArtworkApiClient>(c => c.BaseAddress = new Uri("https://localhost:7215"));
builder.Services.AddHttpClient<BookingApiClient>(c => c.BaseAddress = new Uri("https://localhost:7215"));
builder.Services.AddHttpClient<TicketApiClient>(c => c.BaseAddress = new Uri("https://localhost:7215"));
builder.Services.AddHttpClient<TourApiClient>(c => c.BaseAddress = new Uri("https://localhost:7215"));
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7215") });


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MuseoCivicoAurora.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.MapArtWorksEndpoint();
app.MapExhibitionsEndpoints();
app.MapToursEndpoint();
app.MapTicketsEndpoint();
app.MapBookingsEndpoints();

app.Run();

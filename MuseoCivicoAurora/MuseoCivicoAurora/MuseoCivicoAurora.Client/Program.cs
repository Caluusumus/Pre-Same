using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MuseoCivicoAurora.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();
builder.Services.AddScoped<ExhibitionApiClient>();
builder.Services.AddScoped<ArtworkApiClient>();
builder.Services.AddScoped<BookingApiClient>();
builder.Services.AddScoped<TicketApiClient>();
builder.Services.AddScoped<TourApiClient>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


await builder.Build().RunAsync();

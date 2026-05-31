using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MuseoCivicoAurora.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();
builder.Services.AddScoped<ExhibitionApiClient>();


await builder.Build().RunAsync();

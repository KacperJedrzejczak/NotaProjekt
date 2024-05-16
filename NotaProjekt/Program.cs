using BlazorNotatnik.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NotaProjekt; 

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped<NoteService>(); 

await builder.Build().RunAsync();

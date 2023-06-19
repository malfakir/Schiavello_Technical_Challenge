using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Schiavello_Technical_Challenge;
using Schiavello_Technical_Challenge.IServices;
using Schiavello_Technical_Challenge.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ITodoService,TodoService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped(sp =>
{
    var apiUrl = "http://localhost:5020"; // Replace with the actual API URL
    return new HttpClient { BaseAddress = new Uri(apiUrl) };
});


await builder.Build().RunAsync();

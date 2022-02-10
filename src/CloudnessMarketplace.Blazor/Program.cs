using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CloudnessMarketplace.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Setup an http client with a authoraization message handler to call the Azure Functions 
builder.Services.AddHttpClient("CloudnessMarketplace.Functions", client =>
{
    client.BaseAddress = new Uri("https://cloudnessmarketplace-functions.azurewebsites.net");
    client.BaseAddress = new Uri("https://cloudnessmarketplace-functions.azurewebsites.net");
}).AddHttpMessageHandler<AuthorizationMessageHandler>();

builder.Services.AddTransient<AuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>()?.CreateClient("CloudnessMarketplace.Functions") ?? new HttpClient());

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add(builder.Configuration["AzureAdB2C:DefaultScope"]);
});

await builder.Build().RunAsync();

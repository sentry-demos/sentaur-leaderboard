using System.IO.Compression;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sentaur.Leaderboard.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.UseSentry(o =>
{
    o.Dsn = "https://8f3ccd6a8a8e5ba417de8df962236a7d@o87286.ingest.us.sentry.io/4506888120107008";
    o.TracesSampleRate = 1.0;
});

builder.Logging.AddSentry(o => o.InitializeSdk = false);


// builder.Services.AddScoped(sp => new HttpClient(
//     // Sentry tracing integration:
//     new SentryHttpMessageHandler()) { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

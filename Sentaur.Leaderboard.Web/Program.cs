using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sentaur.Leaderboard.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.UseSentry(o =>
{
    o.Dsn = "https://de39606043c0b0a7482ebd54f060871f@o87286.ingest.us.sentry.io/4508985365037056";
    o.TracesSampleRate = 1.0;
    o.Release = "1.0.25";
    o.Debug = true;
});

builder.Services.AddScoped(sp => new HttpClient(
    // Sentry tracing integration:
    new SentryHttpMessageHandler()) { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

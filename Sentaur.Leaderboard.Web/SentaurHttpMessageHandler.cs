using Microsoft.JSInterop;

namespace Sentaur.Leaderboard.Web;

public class SentaurHttpMessageHandler : DelegatingHandler
{
    private readonly IJSRuntime _runtime;

    public SentaurHttpMessageHandler(IJSRuntime runtime)
    {
        _runtime = runtime;
        InnerHandler = new HttpClientHandler();
    }
    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var method = request.Method.Method.ToUpperInvariant();
        var url = request.RequestUri?.ToString() ?? string.Empty;

        // var pageLoadTransaction = Sentry.getCurrentHub().getScope().getTransaction().toTraceparent();
        // const sentryTraceHeader = pageLoadTransaction.toTraceparent();
        //
        // var bla = await _runtime.InvokeAsync<string>("Sentry.getCurrentHub");
        Console.WriteLine("sync http");
        // request.Headers.Add();
        return base.Send(request, cancellationToken);
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var sentryTrace = await _runtime.InvokeAsync<string>("sentryTrace", cancellationToken);
        Console.WriteLine("trace: " + sentryTrace);

        request.Headers.Add("sentry-trace", sentryTrace);

        return await base.SendAsync(request, cancellationToken);
    }
}

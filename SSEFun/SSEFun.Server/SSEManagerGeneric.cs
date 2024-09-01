using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
namespace SSEFun.SSEThings;

public class SSEManagerGeneric
{
    public IEnumerable<Guid> UserIds => _clients.Keys.ToArray();
    private ConcurrentDictionary<Guid, HttpResponse> _clients = [];

    public async Task ProlongedConnection(Guid userId, HttpResponse clientResponse, CancellationToken cancellationToken)
    {
        _clients.TryAdd(userId, clientResponse);

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // SSE is a long-lived HTTP connection. 
                await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            await this.Disconnect(userId);
        }

        // Disconnect in case the token was cancelled, but no exception was thrown. 
        await this.Disconnect(userId);
    }

    public async Task Disconnect(Guid userId)
    {
        _clients.TryRemove(userId, out var result);
    }

    public async Task FlushToUser<T>(Guid userId, T message)
    {
        _clients.TryGetValue(userId, out var clientResponse);

        var rawMessage = SSEMessaging.ToReadableLine(message);

        await clientResponse.WriteAsync(rawMessage);

        await clientResponse.Body.FlushAsync();
    }
}

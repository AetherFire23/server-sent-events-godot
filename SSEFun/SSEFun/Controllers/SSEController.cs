using Microsoft.AspNetCore.Mvc;
using SSEFun.Flushing;
using SSEFun.SSEThings;
// https://chrlschn.dev/blog/2023/09/server-sent-events-using-dotnet-7-web-api/
namespace SSEFun.Controllers;

[ApiController]
[Route("[controller]")]
public class SSEController(SSEManager sseManager) : ControllerBase
{
    [HttpGet]
    public async Task StartSSE([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        await sseManager.ProlongedConnection(userId, this.HttpContext.Response, cancellationToken);
    }
}

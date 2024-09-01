using Microsoft.AspNetCore.Mvc;
using SSEFun.SSEThings;

namespace SSEConsumer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(SSEManagerGeneric generic) : ControllerBase
    {
        [HttpGet("SSE")]
        public async Task SSEConnection(Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                await generic.ProlongedConnection(userId, this.Response, cancellationToken);

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }
    }
}

namespace SampleRestApi.Middlewares;

public class ControlRequestsMiddleware
{
    readonly RequestDelegate next;
    readonly int maxRequestsPerSecond;

    SemaphoreSlim semaphore = new(1);
    DateTime lastRequestTime = DateTime.MinValue;
    int requestCount = 0;
    
    const int oneSecond = 1;

    public ControlRequestsMiddleware(RequestDelegate next, int maxRequestsPerSecond)
    {
        if (maxRequestsPerSecond <= 0)
            throw new ArgumentException($"Incorrect {nameof(maxRequestsPerSecond)}");
        this.next = next;
        this.maxRequestsPerSecond = maxRequestsPerSecond;
    }

    public async Task Invoke(HttpContext context)
    {
        await semaphore.WaitAsync();

        try
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan elapsed = currentTime - lastRequestTime;

            if (elapsed.TotalSeconds >= oneSecond)
            {
                requestCount = 0;
                lastRequestTime = currentTime;
            }
            else if (requestCount >= maxRequestsPerSecond)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                return;
            }

            requestCount++;
        }
        finally
        {
            semaphore.Release();
        }

        await next(context);
    }
}

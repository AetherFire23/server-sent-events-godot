namespace SSEFun.Server;

public interface ISender<T>
{
    public Task<T> GetPersonalizedFlushData(Guid userId);
    //return (userId, new Something() { Test = DateTime.Now.ToFileTimeUtc().ToString() });
}

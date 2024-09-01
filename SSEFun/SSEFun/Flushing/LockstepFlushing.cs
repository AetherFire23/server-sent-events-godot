namespace SSEFun.Flushing;

public interface ILockstepFlusher<TGen>
{
    public Task<TGen> GetPersonalizedFlushData(Guid userId);
}

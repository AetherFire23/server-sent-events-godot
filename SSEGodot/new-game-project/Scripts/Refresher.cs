using System;
using System.Collections.Generic;
using System.Linq;

namespace NewGameProject.Scripts;

public static class Refresher
{
    public static (IEnumerable<Guid> appeared, IEnumerable<Guid> disappeared)
        GetRefreshers(IEnumerable<Guid> oldState, IEnumerable<Guid> newState)
    {
        var appeared = newState.Except(oldState);
        var disappeared = oldState.Except(newState);

        return (appeared, disappeared);
    }
}
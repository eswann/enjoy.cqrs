using System;

namespace Cars.Testing.Shared.EventStore
{
    [Flags]
    public enum EventStoreMethods
    {
        Ctor = 0,
        Dispose = 1,
        BeginTransaction = 2,
        Rollback = 4,
        CommitAsync = 8,
        SaveAsync = 16,
        GetEventsByAggregateId = 32,
        GetEventsByTypeAsync = 64
    }
}
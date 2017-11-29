using System;
using Cars.Attributes;
using Cars.Events;

namespace Cars.UnitTests.Domain.Stubs.Events
{
    [EventName("StubCreated")]
    public class StubStreamCreatedEvent : DomainEvent
    {
        public string Name { get; }

        public StubStreamCreatedEvent(Guid aggregateId, string name) : base(aggregateId)
        {
            Name = name;
        }
    }
}
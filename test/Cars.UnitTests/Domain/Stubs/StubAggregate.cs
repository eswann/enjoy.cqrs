﻿using System;
using Cars.EventSource;
using Cars.UnitTests.Domain.Stubs.Events;

namespace Cars.UnitTests.Domain.Stubs
{
    public class StubAggregate : Aggregate
    {
        public string Name { get; private set; }
        public Guid RelatedId { get; private set; }

        private StubAggregate(Guid newGuid, string name)
        {
            Emit(new StubAggregateCreatedEvent(newGuid, name));
        }

        public StubAggregate()
        {
        }

        public static StubAggregate Create(string name)
        {
            return new StubAggregate(Guid.NewGuid(), name);
        }

        public void ChangeName(string name)
        {
            Emit(new NameChangedEvent(AggregateId, name));
        }

        public void DoSomethingWithoutEventSubscription()
        {
            Emit(new NotRegisteredEvent(AggregateId));
        }

        public void Relationship(Guid relatedId)
        {
            Emit(new StubAggregateRelatedEvent(AggregateId, relatedId));
        }

        protected override void RegisterEvents()
        {
            SubscribeTo<NameChangedEvent>(x => { Name = x.Name; });
            SubscribeTo<StubAggregateCreatedEvent>(x =>
            {
                AggregateId = x.AggregateId;
                Name = x.Name;
            });

            SubscribeTo<StubAggregateRelatedEvent>(x =>
            {
                RelatedId = x.StubAggregateId;
            });
        }

    }
}
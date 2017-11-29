﻿using System;
using Cars.Events;

namespace Cars.Testing.Shared.StubApplication.Domain.Bar
{
    public class SpokeSomething : IDomainEvent
    {
        public string Text { get; }

        public SpokeSomething(string text)
        {
            Text = text;
        }

        public Guid AggregateId { get; } = Guid.NewGuid();
    }
}
﻿using System;
using Cars.Events;

namespace Cars.Demo.Domain.Events
{
    public class CartCreated : DomainEvent
    {
        public CartCreated(Guid aggregateId, string userId) : base(aggregateId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
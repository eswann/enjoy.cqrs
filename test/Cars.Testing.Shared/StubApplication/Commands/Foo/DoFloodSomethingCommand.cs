﻿using System;
using Cars.Commands;

namespace Cars.Testing.Shared.StubApplication.Commands.Foo
{
    public class DoFloodSomethingCommand : Command
    {
        public int Times { get; }

        public DoFloodSomethingCommand(Guid aggregateId, int times) : base(aggregateId)
        {
            Times = times;
        }
    }
}
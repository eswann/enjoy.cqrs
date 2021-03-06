﻿using System;
using System.Threading.Tasks;
using Cars.Commands;
using Cars.Testing.Shared.MessageBus;
using Cars.UnitTests.Domain.Stubs;
using FluentAssertions;
using Xunit;

namespace Cars.UnitTests.Handlers
{
    public class CommandHandlerTests : CommandTestFixture<CommandHandlerTests.CreateStubCommand, DefaultResponse, CommandHandlerTests.CreateStubCommandHandler, StubAggregate>
    {
        private const string _categoryName = "Unit";
        private const string _categoryValue = "Handlers";

        private Guid _id;

        protected override CreateStubCommand When()
        {
            _id = Guid.NewGuid();
            return new CreateStubCommand(_id);
        }
        
        [Trait(_categoryName, _categoryValue)]
        [Fact]
        public void Executed_property_should_be_true()
        {
            CommandHandler.Executed.Should().Be(true);
        }

        [Trait(_categoryName, _categoryValue)]
        [Fact]
        public void Should_pass_the_correct_AggregateId()
        {
            CommandHandler.AggregateId.Should().Be(_id);
        }

        public class CreateStubCommand : CommandBase
        {
            public CreateStubCommand(Guid aggregateId) : base(aggregateId)
            {
            }
        }

        public class CreateStubCommandHandler : ICommandHandler<CreateStubCommand, DefaultResponse>
        {
            public bool Executed { get; set; }
            public Guid AggregateId { get; set; }

            public Task<DefaultResponse> ExecuteAsync(CreateStubCommand command)
            {
                Executed = true;
                AggregateId = command.AggregateId;

	            return Task.FromResult(new DefaultResponse(command.AggregateId));
            }
        }
    }
}
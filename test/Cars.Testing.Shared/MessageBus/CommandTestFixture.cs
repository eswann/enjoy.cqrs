﻿// The MIT License (MIT)
// 
// Copyright (c) 2016 Nelson Corrêa V. Júnior
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cars.Collections;
using Cars.Commands;
using Cars.Events;
using Cars.EventSource;
using Cars.EventSource.Storage;
using Moq;

namespace Cars.Testing.Shared.MessageBus
{
    public abstract class CommandTestFixture<TCommand, TResponse, TCommandHandler, TAggregate>
        where TCommand : class
        where TCommandHandler : class, ICommandHandler<TCommand, TResponse>
        where TAggregate : Aggregate, new()
    {
        private readonly IDictionary<Type, object> _mocks;

        protected TAggregate AggregateRoot;
        protected TCommandHandler CommandHandler;
        protected Exception CaughtException;
        protected IEnumerable<IDomainEvent> PublishedEvents;
        protected virtual void SetupDependencies() { }
        protected virtual IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }
        protected virtual void Finally() { }
        protected abstract TCommand When();

        protected CommandTestFixture()
        {
            _mocks = new Dictionary<Type, object>();
            CaughtException = new ThereWasNoExceptionButOneWasExpectedException();
            AggregateRoot = new TAggregate();
            AggregateRoot.LoadFromHistory(new CommitedDomainEventCollection(Given()));

            CommandHandler = BuildHandler();

            SetupDependencies();

            try
            {
                CommandHandler.ExecuteAsync(When()).GetAwaiter().GetResult();
                PublishedEvents = AggregateRoot.UncommitedEvents.Select(e => e.OriginalEvent);
            }
            catch (Exception exception)
            {
                CaughtException = exception;
            }
            finally
            {
                Finally();
            }
        }

        public Mock<TType> OnDependency<TType>() where TType : class
        {
            return (Mock<TType>)_mocks[typeof(TType)];
        }

        private TCommandHandler BuildHandler()
        {
            var constructorInfo = typeof(TCommandHandler).GetConstructors().First();

            foreach (var parameter in constructorInfo.GetParameters())
            {
                if (parameter.ParameterType == typeof(IRepository))
                {
                    var repositoryMock = new Mock<IRepository>();
                    repositoryMock.Setup(x => x.GetByIdAsync<TAggregate>(It.IsAny<Guid>())).Returns(Task.FromResult(AggregateRoot));
                    repositoryMock.Setup(x => x.Add(It.IsAny<TAggregate>())).Callback<TAggregate>(x => AggregateRoot = x);
                    _mocks.Add(parameter.ParameterType, repositoryMock);
                    continue;
                }

                _mocks.Add(parameter.ParameterType, CreateMock(parameter.ParameterType));
            }

            return (TCommandHandler)constructorInfo.Invoke(_mocks.Values.Select(x => ((Mock)x).Object).ToArray());
        }

        private static object CreateMock(Type type)
        {
            var constructorInfo = typeof(Mock<>).MakeGenericType(type).GetConstructors().First();
            return constructorInfo.Invoke(new object[] { });
        }
    }

    public class ThereWasNoExceptionButOneWasExpectedException : Exception { }
}
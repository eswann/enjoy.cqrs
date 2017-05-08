﻿using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace EnjoyCQRS.Microsoft.Logging.UnitTests.Stubs
{
    public class TestLoggerFactory : ILoggerFactory
    {
        private readonly bool _enabled;
        private readonly IList<TestMessage> _messages;

        public TestLoggerFactory(bool enabled, IList<TestMessage> messages)
        {
            _enabled = enabled;
            _messages = messages;
        }

        public ILogger CreateLogger(string name)
        {
            return new TestLogger(name, _enabled, _messages);
        }

        public void AddProvider(ILoggerProvider provider)
        {
        }

        public void Dispose()
        {
        }
    }
}

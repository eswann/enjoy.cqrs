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
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Cars.EventSource.Storage
{
    public class Repository : IRepository
    {
        private readonly ISession _session;
        private readonly ILogger _logger;

        public Repository(ILoggerFactory loggerFactory, ISession session)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _logger = loggerFactory?.CreateLogger<Repository>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public Task AddAsync<TAggregate>(TAggregate projection) where TAggregate : Mutator
        {
            _logger.LogDebug($"Called method: {nameof(Repository)}.{nameof(AddAsync)}.");

            return _session.AddAsync(projection);
        }

        public Task<TProjection> GetByIdAsync<TProjection>(Guid id) where TProjection : Projection, new()
        {
            _logger.LogDebug($"Called method: {nameof(Repository)}.{nameof(GetByIdAsync)}.");

            return _session.GetByIdAsync<TProjection>(id);
        }
    }
}

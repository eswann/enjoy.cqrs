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

namespace Cars.EventSource.Storage
{
    /// <summary>
    /// Represents an abstraction where an instance of <see cref="Aggregate"/> will be persisted.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Retrieves an <typeparam name="TProjection"></typeparam> based on your unique identifier property.
        /// </summary>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="id"></param>
        Task<TProjection> GetByIdAsync<TProjection>(Guid id) where TProjection : AggregateProjection, new();

        /// <summary>
        /// Add an instance of <typeparam name="TAggregate"></typeparam> in repository.
        /// </summary>
        /// <typeparam name="TAggregate"></typeparam>
        /// <param name="aggregate"></param>
        Task AddAsync<TAggregate>(TAggregate aggregate) where TAggregate : Aggregate;
    }
}

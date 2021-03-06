﻿using System;
using System.Threading.Tasks;
using Cars.Projections;

namespace Cars.Demo.Query.Services.Carts
{
    public class CartRepository : ICartRepository
    {
        private readonly IProjectionRepository _repository;

        public CartRepository(IProjectionRepository repository)
        {
            _repository = repository;
        }

        public Task<CartProjection> GetProjectionAsync(Guid cartId)
        {
            return _repository.RetrieveAsync<CartProjection>(cartId);
        }
    }
}
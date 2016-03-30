﻿using System.Linq;
using MyCQRS.Events;
using MyCQRS.Restaurant.Read;
using MyCQRS.Restaurant.Read.Models;

namespace MyCQRS.Restaurant.Events.Handlers
{
    public class DrinksServedEventHandler : IEventHandler<DrinksServedEvent>
    {
        private readonly IReadRepository<TabModel> _repository;

        public DrinksServedEventHandler(IReadRepository<TabModel> repository)
        {
            _repository = repository;
        }

        public void Execute(DrinksServedEvent theEvent)
        {
            var tab = _repository.GetById(theEvent.AggregateId);

            foreach (var menuNumber in theEvent.MenuNumbers)
            {
                var order = tab.OrderedItems.FirstOrDefault(e => e.Status == "Ordered" && e.MenuNumber == menuNumber);

                if (order != null)
                {
                    order.Status = "Served";

                    tab.OrderedValue += order.Price;
                }
            }

            _repository.Update(tab);
        }
    }
}
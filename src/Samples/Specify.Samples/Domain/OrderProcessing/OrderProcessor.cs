using System;

namespace Specify.Samples.Domain.OrderProcessing
{
    public class OrderProcessor
    {
        private readonly IInventory _inventory;
        private readonly Publisher _publisher;

        public OrderProcessor(IInventory inventory, Publisher publisher)
        {
            _inventory = inventory;
            _publisher = publisher;
        }

        public virtual OrderResult Process(Order order)
        {
            var result = new OrderResult();

            if (order.Quantity < 0)
            {
                return result;
            }

            var available = _inventory.IsQuantityAvailable(order.PartNumber, order.Quantity);

            if (available)
            {
                result.WasAccepted = true;

                var orderNumber = Guid.NewGuid().ToString();

                _publisher.Publish(new OrderSubmitted { OrderNumber = orderNumber });

                result.OrderNumber = orderNumber;
            }

            return result;
        }
    }
}
using System;
using Serilog;

namespace Specs.Unit.ApiTemplate.Samples
{
    public class OrderProcessor
    {
        private readonly IInventory _inventory;
        private readonly Publisher _publisher;
        private readonly Auditer _auditer;

        public OrderProcessor(IInventory inventory, Publisher publisher, Auditer auditer)
        {
            _inventory = inventory;
            _publisher = publisher;
            _auditer = auditer;
        }

        public virtual OrderResult Process(Order order)
        {
            _auditer.Audit(order);
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
    public sealed class Auditer
    {
        private readonly ILogger _logger;

        public Auditer(ILogger logger)
        {
            _logger = logger;
        }

        public void Audit(Order order)
        {
            _logger.Information(order.PartNumber);
        }
    }
   
    public class Order
    {
        public string PartNumber { get; set; }

        public int Quantity { get; set; }
    }
   
    public interface IInventory
    {
        bool IsQuantityAvailable(string partNumber, int quantity);
    }
   
    public class OrderResult
    {
        public bool WasAccepted { get; set; }

        public string OrderNumber { get; set; }
    }
    
    public class OrderSubmitted
    {
        public string OrderNumber { get; set; }
    }
   
    public class Publisher
    {
        public virtual void Publish<TEvent>(TEvent @event)
        {
        }
    }
}

using Castle.Core.Logging;

namespace Specify.Samples.Domain.OrderProcessing
{
    public sealed class Auditer
    {
        private readonly ILogger _logger;

        public Auditer(ILogger logger)
        {
            _logger = logger;
        }

        public void Audit(Order order)
        {
            _logger.Info(order.PartNumber);
        }
    }
}

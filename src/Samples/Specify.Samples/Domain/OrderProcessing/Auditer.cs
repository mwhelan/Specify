using Serilog;

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
            _logger.Information(order.PartNumber);
        }
    }
}

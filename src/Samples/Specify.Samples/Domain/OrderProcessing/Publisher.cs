namespace Specify.Samples.Domain.OrderProcessing
{
	public class Publisher
	{
        public virtual void Publish<TEvent>(TEvent @event)
        {
        }
    }
}
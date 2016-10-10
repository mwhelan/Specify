namespace Specify.Samples.Domain.OrderProcessing
{
	public interface IPublisher
	{
		void Publish<TEvent>(TEvent @event);
	}
}
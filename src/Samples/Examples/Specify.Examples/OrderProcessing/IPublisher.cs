namespace Specify.Examples.OrderProcessing
{
	public interface IPublisher
	{
		void Publish<TEvent>(TEvent @event);
	}
}
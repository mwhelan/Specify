namespace Specify.Tests.Example.OrderProcessing
{
	public interface IPublisher
	{
		void Publish<TEvent>(TEvent @event);
	}
}
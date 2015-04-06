namespace Specify.IntegrationTests.App.OrderProcessing
{
	public interface IPublisher
	{
		void Publish<TEvent>(TEvent @event);
	}
}
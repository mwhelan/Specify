namespace Specify.Samples.OrderProcessing
{
	public interface IPublisher
	{
		void Publish<TEvent>(TEvent @event);
	}
}
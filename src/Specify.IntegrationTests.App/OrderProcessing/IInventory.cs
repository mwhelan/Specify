namespace Specify.IntegrationTests.App.OrderProcessing
{
	public interface IInventory
	{
		bool IsQuantityAvailable(string partNumber, int quantity);
	}
}
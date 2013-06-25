namespace Specify.Tests.Example.OrderProcessing
{
	public interface IInventory
	{
		bool IsQuantityAvailable(string partNumber, int quantity);
	}
}
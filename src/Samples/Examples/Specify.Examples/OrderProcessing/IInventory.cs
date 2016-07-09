namespace Specify.Examples.OrderProcessing
{
	public interface IInventory
	{
		bool IsQuantityAvailable(string partNumber, int quantity);
	}
}
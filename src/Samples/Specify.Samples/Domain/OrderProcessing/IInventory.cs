namespace Specify.Samples.Domain.OrderProcessing
{
	public interface IInventory
	{
		bool IsQuantityAvailable(string partNumber, int quantity);
	}
}
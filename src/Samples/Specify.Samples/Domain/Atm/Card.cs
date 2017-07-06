namespace Specify.Samples.Domain.Atm
{
	public class Card
	{
		public virtual int AccountBalance { get; set; }
		private readonly bool _enabled;

		public Card(bool enabled, int accountBalance)
		{
			AccountBalance = accountBalance;
			_enabled = enabled;
		}

		protected Card() { }

		public virtual bool Enabled
		{
			get { return _enabled; }
		}
	}
}
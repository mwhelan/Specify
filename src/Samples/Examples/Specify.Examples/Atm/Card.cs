namespace Specify.Examples.ATM
{
    public class Card
    {
        public int AccountBalance { get; set; }
        private readonly bool _enabled;

        public Card(bool enabled, int accountBalance)
        {
            AccountBalance = accountBalance;
            _enabled = enabled;
        }

        public bool Enabled
        {
            get { return _enabled; }
        }
    }
}
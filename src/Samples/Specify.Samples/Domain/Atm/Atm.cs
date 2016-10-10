namespace Specify.Samples.Domain.Atm
{
    public class Atm
    {
        public int ExistingCash { get; private set; }

        public Atm(int existingCash)
        {
            ExistingCash = existingCash;
        }

        public void RequestMoney(Card card, int request)
        {
            if (!card.Enabled)
            {
                CardIsRetained = true;
                Message = DisplayMessage.CardIsRetained;
                return;
            }

            if(card.AccountBalance < request)
            {
                Message = DisplayMessage.InsufficientFunds;
                return;
            }

            DispenseValue = request;
            card.AccountBalance -= request;
        }

        public int DispenseValue { get; set; }

        public bool CardIsRetained { get; private set; }

        public DisplayMessage Message { get; private set; }
    }
}
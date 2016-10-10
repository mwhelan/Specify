namespace Specify.Samples.Domain.TrainFares
{
    public class SingleTicket : Fare
    {
        public override string ToString()
        {
            return "Single Ticket";
        }

        protected override Money Cost => new Money(3);

    }
}
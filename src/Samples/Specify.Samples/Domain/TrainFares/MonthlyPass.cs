namespace Specify.Samples.Domain.TrainFares
{
    public class MonthlyPass : Fare
    {
        public override string ToString()
        {
            return "Monthly Pass";
        }

        protected override Money Cost => new Money(150);

    }
}
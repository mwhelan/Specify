namespace Specify.Samples.Domain.TrainFares
{
    public class DayPass : Fare
    {
        public override string ToString()
        {
            return "Day Pass";
        }

        protected override Money Cost => new Money(10);
    }
}
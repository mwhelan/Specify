namespace Specify.Samples.Domain.TrainFares
{
    public class WeeklyPass : Fare
    {
        public override string ToString()
        {
            return "Weekly Pass";
        }

        protected override Money Cost => new Money(50);

    }
}
namespace Specify.Samples.Domain.TrainFares
{
    public abstract class Fare
    {
        protected abstract Money Cost { get; }
    }
}
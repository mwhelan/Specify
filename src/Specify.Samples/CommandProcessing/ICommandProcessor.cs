namespace Specify.Samples.CommandProcessing
{
    public interface ICommandProcessor
    {
        ExecutionResult Execute<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
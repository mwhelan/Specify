namespace Specify.Tests.Example.CommandProcessing
{
    public interface ICommandProcessor
    {
        ExecutionResult Execute<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}
namespace Specify.Samples.CommandProcessing
{
    public interface IValidateCommand
    {
        ExecutionResult Validate(object command);
    }

    public interface IValidateCommand<TCommand> : IValidateCommand where TCommand : ICommand
    {
        TCommand Command { get; set; }
    }
}

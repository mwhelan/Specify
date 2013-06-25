namespace Specify.Tests.Example.CommandProcessing
{
    public interface IHandleCommand
    {
        void Handle();
    }

    public interface IHandleCommand<TCommand> : IHandleCommand where TCommand : ICommand
    {
        TCommand Command { get; set; }
    }
}

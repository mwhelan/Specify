namespace Specify.Tests.Example.CommandProcessing
{
    public interface IHandleCommandFactory
    {
        IHandleCommand<T> HandlerForCommand<T>(T command) where T : ICommand;
        void Release(IHandleCommand component);
    }
}
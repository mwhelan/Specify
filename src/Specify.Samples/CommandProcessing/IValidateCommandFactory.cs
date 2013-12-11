namespace Specify.Samples.CommandProcessing
{
    public interface IValidateCommandFactory
    {
        IValidateCommand<T> ValidatorForCommand<T>(T command) where T : ICommand;
        void Release(IValidateCommand component);
    }
}
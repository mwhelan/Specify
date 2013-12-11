namespace Specify.Samples.CommandProcessing
{
    public interface ILog
    {
        void Info(string message);
    }
    public class CommandProcessor : ICommandProcessor
    {
        public ILog Logger { get; set; }
        private IHandleCommandFactory _handleCommandFactory;
        private IValidateCommandFactory _validatorFactory;
        private ExecutionResult _executionResult;

        public CommandProcessor(IHandleCommandFactory handleCommandFactory, IValidateCommandFactory validatorFactory)
        {
            _handleCommandFactory = handleCommandFactory;
            _validatorFactory = validatorFactory;
        }

        public ExecutionResult Execute<T>(T command) where T : class, ICommand
        {
            //Guard.That(() => command).IsNotNull();
            _executionResult = new ExecutionResult(command);

            Validate(command);

            if (_executionResult.IsSuccessful)
            {
                HandlerFor(command)
                    .Handle();
            }

            Logger.Info(_executionResult.ToString());
            return _executionResult;
        }

        private void Validate<T>(T command) where T : class, ICommand
        {
            var validator = _validatorFactory.ValidatorForCommand<T>(command);
            if (validator == null)
                return;
            ExecutionResult validationResult = validator.Validate(command);
            if (!validationResult.IsSuccessful)
                _executionResult.AddErrors(validationResult.Errors);
        }

        private IHandleCommand<T> HandlerFor<T>(T command) where T : class, ICommand
        {
            var handler = _handleCommandFactory.HandlerForCommand<T>(command);
            if (handler == null)
                throw new  FactoryItemNotFoundException(typeof(T));
            return handler;
        }

        //private ILogger _logger = NullLogger.Instance;
        //public ILogger Logger
        //{
        //    get { return _logger; }
        //    set { _logger = value; }
        //}

    }
}

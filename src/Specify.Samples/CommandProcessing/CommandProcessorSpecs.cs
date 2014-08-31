using NSubstitute;
using Shouldly;

namespace Specify.Samples.CommandProcessing
{
    public class given
    {
        public abstract class the_command_is_valid : SpecificationFor<CommandProcessor>
        {
            protected void Given_the_command_is_valid()
            {
                DependencyFor<IValidateCommand<TestCommand>>().Validate(Arg.Any<TestCommand>()).Returns(new ExecutionResult(null));
                DependencyFor<IValidateCommandFactory>().ValidatorForCommand(Arg.Any<TestCommand>()).Returns(DependencyFor<IValidateCommand<TestCommand>>());
            }
        }
    }

    public class processing_a__valid_command : given.the_command_is_valid
    {
        private TestCommand _command = new TestCommand();
        private ExecutionResult _result;

        public void when_processing_a_valid_command()
        {
            _result = SUT.Execute(_command);
        }

        public void Then_the_processor_should_find_the_validator_for_the_command()
        {
            DependencyFor<IValidateCommandFactory>().Received().ValidatorForCommand(_command);
        }

        public void AndThen_validate_the_command()
        {
            DependencyFor<IValidateCommand<TestCommand>>().Received().Validate(_command);
        }

        public void AndThen_the_processor_should_find_the_handler_for_the_command()
        {
            DependencyFor<IHandleCommandFactory>().Received().HandlerForCommand(_command);
        }

        public void AndThen_the_command_is_processed_successfully()
        {
            _result.IsSuccessful.ShouldBe(true);
        }

        public void AndThen_the_result_is_logged()
        {
            DependencyFor<ILog>().Received().Info(Arg.Any<string>());
        }
    }
}

using FluentAssertions;
using NSubstitute;

namespace Specify.Samples.CommandProcessing
{
    public class given
    {
        public abstract class the_command_is_valid : SpecificationFor<CommandProcessor>
        {
            protected void Given_the_command_is_valid()
            {
                SubFor<IValidateCommand<TestCommand>>().Validate(Arg.Any<TestCommand>()).Returns(new ExecutionResult(null));
                SubFor<IValidateCommandFactory>().ValidatorForCommand(Arg.Any<TestCommand>()).Returns(SubFor<IValidateCommand<TestCommand>>());
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
            SubFor<IValidateCommandFactory>().Received().ValidatorForCommand(_command);
        }

        public void AndThen_validate_the_command()
        {
            SubFor<IValidateCommand<TestCommand>>().Received().Validate(_command);
        }

        public void AndThen_the_processor_should_find_the_handler_for_the_command()
        {
            SubFor<IHandleCommandFactory>().Received().HandlerForCommand(_command);
        }

        public void AndThen_the_command_is_processed_successfully()
        {
            _result.IsSuccessful.Should().BeTrue();
        }

        public void AndThen_the_result_is_logged()
        {
            SubFor<ILog>().Received().Info(Arg.Any<string>());
        }
    }
}

using Specify.Stories;

namespace Specs.Acceptance._CurrentSprint.MasterFiles._BaseSpecifications
{
    public abstract class MasterFilesStory : UserStory
    {
        protected abstract string EntityName { get; }

        protected MasterFilesStory()
        {
            AsA = "As a Farm Manager";
            IWant = $"I want to update my {EntityName} Master Files";
            SoThat = "So that I can add list data that is custom to my organisation";
            TitlePrefix = $"Story: Master Files - ";
        }
    }
}
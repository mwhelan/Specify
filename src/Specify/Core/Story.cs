namespace Specify.Core
{
    public abstract class UserStory
    {
        protected UserStory()
        {
            TitlePrefix = "Story: ";
        }
        public abstract string Title { get; }
        public virtual string TitlePrefix { get; set; }
        public abstract string AsA { get; }
        public abstract string IWantTo { get; }
        public abstract string SoThat { get; }
    }
}
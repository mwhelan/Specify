namespace Specify.Stories
{
    public abstract class ValueStory : Story
    {
        private const string In_order_to_prefix = "In order to";
        private const string As_a_prefix = "As a";
        private const string I_want_prefix = "I want";

        public string InOrderTo
        {
            get { return Narrative1; }
            set { Narrative1 = CleanseProperty(value, In_order_to_prefix); }
        }

        public string AsA
        {
            get { return Narrative2; }
            set { Narrative2 = CleanseProperty(value, As_a_prefix); }
        }

        public string IWant
        {
            get { return Narrative3; }
            set { Narrative3 = CleanseProperty(value, I_want_prefix); }
        }
    }
}
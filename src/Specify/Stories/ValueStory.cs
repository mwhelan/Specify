namespace Specify.Stories
{
    /// <summary>
    /// A Business Value user story template base class for a BDDfy Story allowing representation as a class rather than an attribute.
    /// </summary>
    public abstract class ValueStory : Story
    {
        private const string In_order_to_prefix = "In order to";
        private const string As_a_prefix = "As a";
        private const string I_want_prefix = "I want";

        /// <summary>
        /// Gets or sets the 'In order to' clause.
        /// </summary>
        /// <value>The in order to clause.</value>
        public string InOrderTo
        {
            get { return Narrative1; }
            set { Narrative1 = CleanseProperty(value, In_order_to_prefix); }
        }

        /// <summary>
        /// Gets or sets the 'As a' clause.
        /// </summary>
        /// <value>The in order to clause.</value>
        public string AsA
        {
            get { return Narrative2; }
            set { Narrative2 = CleanseProperty(value, As_a_prefix); }
        }

        /// <summary>
        /// Gets or sets the 'I want' clause.
        /// </summary>
        /// <value>The in order to clause.</value>
        public string IWant
        {
            get { return Narrative3; }
            set { Narrative3 = CleanseProperty(value, I_want_prefix); }
        }
    }
}
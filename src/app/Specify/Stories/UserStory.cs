namespace Specify.Stories
{
    /// <summary>
    /// A User Story template base class for a BDDfy Story allowing representation as a class rather than an attribute.
    /// </summary>
    public abstract class UserStory : Story
    {
        private const string As_a_prefix = "As a";
        private const string I_want_prefix = "I want";
        private const string So_that_prefix = "So that";

        /// <summary>
        /// Gets or sets the 'As a' clause.
        /// </summary>
        /// <value>As a.</value>
        public string AsA
        {
            get { return Narrative1; }
            set { Narrative1 = CleanseProperty(value, As_a_prefix); }
        }

        /// <summary>
        /// Gets or sets the 'I want' clause.
        /// </summary>
        /// <value>The i want.</value>
        public string IWant
        {
            get { return Narrative2; }
            set { Narrative2 = CleanseProperty(value, I_want_prefix); }
        }

        /// <summary>
        /// Gets or sets the 'So that' clause.
        /// </summary>
        /// <value>The so that.</value>
        public string SoThat
        {
            get { return Narrative3; }
            set { Narrative3 = CleanseProperty(value, So_that_prefix); }
        }
    }
}
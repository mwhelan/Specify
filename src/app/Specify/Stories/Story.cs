using System;
using System.Text;

namespace Specify.Stories
{
    /// <summary>
    /// A base class for a BDDfy Story allowing representation as a class rather than an attribute.
    /// </summary>
    public abstract class Story
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Story"/> class.
        /// </summary>
        protected Story()
        {
            TitlePrefix = "Story: ";
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the title prefix.
        /// </summary>
        /// <value>The title prefix.</value>
        public string TitlePrefix { get; set; }

        /// <summary>
        /// Gets or sets the narrative1.
        /// </summary>
        /// <value>The narrative1.</value>
        public string Narrative1 { get; set; }

        /// <summary>
        /// Gets or sets the narrative2.
        /// </summary>
        /// <value>The narrative2.</value>
        public string Narrative2 { get; set; }

        /// <summary>
        /// Gets or sets the narrative3.
        /// </summary>
        /// <value>The narrative3.</value>
        public string Narrative3 { get; set; }

        /// <summary>
        /// Gets or sets the ImageUri.
        /// </summary>
        /// <value>The ImageUri.</value>
        public string ImageUri { get; set; }

        /// <summary>
        /// Gets or sets the StoryUri.
        /// </summary>
        /// <value>The StoryUri.</value>
        public string StoryUri { get; set; }

        /// <summary>
        /// Cleanses the property.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns>System.String.</returns>
        protected string CleanseProperty(string text, string prefix)
        {
            var property = new StringBuilder();

            if (string.IsNullOrWhiteSpace(text))
                return null;

            if (!text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                property.AppendFormat("{0} ", prefix);

            property.Append(text);
            return property.ToString();
        }
    }
}
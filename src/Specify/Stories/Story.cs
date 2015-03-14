using System;
using System.Text;

namespace Specify.Stories
{
    public abstract class Story
    {
        protected Story()
        {
            TitlePrefix = "Story: ";
        }

        public string Title { get; set; }
        public string TitlePrefix { get; set; }
        public string Narrative1 { get; set; }
        public string Narrative2 { get; set; }
        public string Narrative3 { get; set; }

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
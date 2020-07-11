using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Specs.Library.Extensions
{
    public static class StringExtensions
    {
        public static string Repeat(this string s, int n)
        {
            return new StringBuilder(s.Length * n)
                .AppendJoin(s, new string[n + 1])
                .ToString();
        }

        public static T FromXmlStringToType<T>(this string xml)
        {
            T instance;
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var sr = new StringReader(xml))
            {
                instance = (T)xmlSerializer.Deserialize(sr);
            }
            return instance;
        }
    }
}
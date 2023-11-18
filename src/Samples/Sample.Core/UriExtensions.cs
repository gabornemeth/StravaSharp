using System.Collections.Generic;
using System.Linq;

namespace Sample.Core
{
    public static class UriExtensions
    {
        //
        // Summary:
        //     Replacement for HttpUtility.ParseQueryString
        //
        // Parameters:
        //   input:
        public static IDictionary<string, string> ParseQueryString(this string input)
        {
            var list = new List<KeyValuePair<string, string>>();
            string[] array = input.Split('&');
            for (int i = 0; i < array.Length; i++)
            {
                string[] array2 = array[i].Split('=');
                string key = array2[0];
                string value = string.Join("=", array2.Skip(1));
                list.Add(new KeyValuePair<string, string>(key, value));
            }

            return list.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}

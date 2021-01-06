using System;

namespace Apps.Common.Naming
{
    public static class NamingExtensions
    {
        public static string RemoveFirstSuffix(
            this string value,
            params string[] suffixes)
        {
            foreach (var suffix in suffixes)
            {
                if (value.EndsWith(suffix, StringComparison.InvariantCulture))
                {
                    return value.Substring(0, value.Length - suffix.Length);
                }
            }

            return value;
        }
    }
}
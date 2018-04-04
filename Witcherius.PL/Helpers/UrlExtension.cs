using System.Text.RegularExpressions;

namespace Witcherius.PL.Helpers
{
    public static class UrlExtension
    {
        public static string FriendlyUrl(this string title)
        {
            return Regex.Replace(Regex.Replace(title, @"[^A-Za-z0-9_-]", CharacterTester).Replace(' ', '-').TrimStart('-').TrimEnd('-'), "[-]+", "-").ToLower();
        }

        private static string CharacterTester(Match m)
        {
            var x = m.ToString();
            if (x.Length > 0 && char.IsLetterOrDigit(x[0]))
            {
                return x.ToLower();
            }
            return "-";
        }
    }
}
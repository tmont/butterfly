using System;
using System.Text.RegularExpressions;

namespace ButterflyNet.Parser {
	public static class StringExtensions {
		private static readonly Regex externalUrlRegex = new Regex("https?://", RegexOptions.IgnoreCase);

		public static bool IsExternalUrl(this string s) {
			return !string.IsNullOrEmpty(s) && externalUrlRegex.IsMatch(s);
		}

		public static string NormalizeEol(this string input) {
			if (input == null) {
				throw new ArgumentNullException("input");
			}

			//Unix line endings ftw
			return input.Replace("\r\n", "\n").Replace('\r', '\n');
		}
	}
}
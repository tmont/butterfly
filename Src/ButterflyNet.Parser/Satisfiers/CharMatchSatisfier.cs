using System.Text.RegularExpressions;

namespace ButterflyNet.Parser.Satisfiers {
	public class CharMatchSatisfier : ISatisfier {
		private readonly Regex regex;

		public CharMatchSatisfier(string regex) {
			this.regex = new Regex(regex);	
		}

		public bool IsSatisfiedBy(ParseContext context) {
			return regex.IsMatch(context.Input.Substring);
		}
	}
}
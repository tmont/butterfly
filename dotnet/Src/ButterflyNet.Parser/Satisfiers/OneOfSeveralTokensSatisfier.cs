using System.Linq;

namespace ButterflyNet.Parser.Satisfiers {
	public class OneOfSeveralTokensSatisfier : ISatisfier {
		private readonly char[] characters;

		public OneOfSeveralTokensSatisfier(params char[] characters) {
			this.characters = characters;
		}

		public bool IsSatisfiedBy(ParseContext context) {
			return characters.Any(character => character == context.CurrentChar);
		}
	}
}
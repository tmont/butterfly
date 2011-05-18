using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class WriteNonParsedCharacterStrategy : WriteCharacterStrategy {
		public WriteNonParsedCharacterStrategy() {
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Unescaped, ScopeTypeCache.NoWiki, ScopeTypeCache.Link));
			AddSatisfier<NonParsedCharacterSatisfier>();
		}

		public override int Priority { get { return DefaultPriority - 1; } }

		protected override char GetChar(ParseContext context) {
			context.AdvanceInput();
			return (char)context.CurrentChar;
		}

		private class NonParsedCharacterSatisfier : ISatisfier {
			public bool IsSatisfiedBy(ParseContext context) {
				return context.CurrentChar == ']' && context.Input.Peek() == ']';
			}
		}
	}
}
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("]]")]
	public class WriteEscapedBracketStrategy : WriteCharacterStrategy {
		public WriteEscapedBracketStrategy() {
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Link, ScopeTypeCache.Module, ScopeTypeCache.Unescaped, ScopeTypeCache.Raw));
		}

		protected override char GetChar(ParseContext context) {
			return ']';
		}

		public override int Priority { get { return base.Priority - 1; } }
	}
}
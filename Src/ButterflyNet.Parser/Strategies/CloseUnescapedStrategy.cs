using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	[NonDefault]
	public class CloseUnescapedStrategy : ScopeDrivenStrategy, ITokenProvider {
		public CloseUnescapedStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(ScopeTypeCache.Unescaped));
			AddSatisfier<NextCharacterIsNotTheSameSatisfier>();
		}

		protected override void Execute(ParseContext context) {
			CloseCurrentScope(context);
		}

		public string Token { get { return "]"; } }
	}
}
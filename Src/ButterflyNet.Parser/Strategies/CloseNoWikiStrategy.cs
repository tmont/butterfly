using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class CloseNoWikiStrategy : ScopeDrivenStrategy, ITokenProvider {
		public CloseNoWikiStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(ScopeTypeCache.NoWiki));
			AddSatisfier<NextCharacterIsNotTheSameSatisfier>();
		}

		protected override void Execute(ParseContext context) {
			CloseCurrentScope(context);
		}

		public string Token { get { return "]"; } }
	}
}
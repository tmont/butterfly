using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class CloseNoWikiStrategy : ScopeDrivenStrategy {
		public CloseNoWikiStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(ScopeTypeCache.NoWiki));
			AddSatisfier(new ExactCharMatchSatisfier("]"));
			AddSatisfier<NextCharacterIsNotTheSameSatisfier>();
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}

		public string Token { get { return "]"; } }
	}
}
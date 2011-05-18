using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	[NonDefault]
	public class CloseUnescapedStrategy : ScopeDrivenStrategy {
		public CloseUnescapedStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(ScopeTypeCache.Unescaped));
			AddSatisfier(new ExactCharMatchSatisfier("]"));
			AddSatisfier<NextCharacterIsNotTheSameSatisfier>();
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}
	}
}
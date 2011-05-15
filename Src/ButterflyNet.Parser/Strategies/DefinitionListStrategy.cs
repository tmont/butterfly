using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class DefinitionListStrategy : BlockStrategy, ITokenProvider {
		public DefinitionListStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
			AddPreExecuteSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.DefinitionTerm)));
		}

		protected override void Execute(ParseContext context) {
			if (!context.Scopes.ContainsType(ScopeTypeCache.DefinitionList)) {
				OpenScope(new DefinitionListScope(), context);
			}

			OpenScope(new DefinitionTermScope(), context);
		}

		public string Token { get { return ";"; } }
	}
}
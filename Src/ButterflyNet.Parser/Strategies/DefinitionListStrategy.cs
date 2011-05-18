using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class DefinitionListStrategy : ScopeDrivenStrategy {
		public DefinitionListStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
			AddSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.DefinitionTerm)));
			AddSatisfier(new ExactCharMatchSatisfier(";"));
		}

		protected override void DoExecute(ParseContext context) {
			if (!context.Scopes.ContainsType(ScopeTypeCache.DefinitionList)) {
				OpenScope(new DefinitionListScope(), context);
			}

			OpenScope(new DefinitionTermScope(), context);
		}
	}
}
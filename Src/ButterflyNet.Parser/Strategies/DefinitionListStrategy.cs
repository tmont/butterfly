using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer(";")]
	public class DefinitionListStrategy : ScopeDrivenStrategy {
		public DefinitionListStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
			AddSatisfier<CannotNestInsideInlineSatisfier>();
			AddSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.DefinitionTerm)));
		}

		protected override void DoExecute(ParseContext context) {
			if (!context.Scopes.ContainsType(ScopeTypeCache.DefinitionList)) {
				OpenScope(new DefinitionListScope(), context);
			}

			OpenScope(new DefinitionTermScope(), context);
		}
	}
}
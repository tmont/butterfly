using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer(":")]
	public class DefinitionStrategy : ScopeDrivenStrategy {
		public DefinitionStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.DefinitionList));
			AddSatisfier<TermMustBePreviousNodeSatisfier>();
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new DefinitionScope(), context);
		}

		private class TermMustBePreviousNodeSatisfier : ISatisfier {
			public bool IsSatisfiedBy(ParseContext context) {
				var previousNode = context.ScopeTree.GetMostRecentNode(depth: context.Scopes.Count);
				return previousNode != null && previousNode.Scope.GetType() == ScopeTypeCache.DefinitionTerm;
			}
		}
	}
}
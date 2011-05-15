using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class DefinitionStrategy : BlockStrategy, ITokenProvider {
		public DefinitionStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.DefinitionList));
			AddPreExecuteSatisfier<TermMustBePreviousNodeSatisfier>();
		}

		protected override void Execute(ParseContext context) {
			OpenScope(new DefinitionScope(), context);
		}

		public string Token { get { return ":"; } }

		private class TermMustBePreviousNodeSatisfier : ISatisfier {
			public bool IsSatisfiedBy(ParseContext context) {
				var previousNode = context.ScopeTree.GetMostRecentNode(depth: context.Scopes.Count);
				return previousNode != null && previousNode.Scope.GetType() == ScopeTypeCache.DefinitionTerm;
			}
		}
	}
}
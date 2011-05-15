using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class OpenMultiLineDefinitionStrategy : BlockStrategy, ITokenProvider {
		public OpenMultiLineDefinitionStrategy() {
			AddSatisfier<DependentSatisfier<DefinitionStrategy>>();
		}

		public override int Priority { get { return DefaultPriority - 1; } }

		protected override void Execute(ParseContext context) {
			OpenScope(new MultiLineDefinitionScope(), context);
		}

		public string Token { get { return ":{"; } }
	}

	public class CloseMultiLineDefinitionStrategy : BlockStrategy, ITokenProvider {
		public CloseMultiLineDefinitionStrategy() {
			AddSatisfier(new LastNonContextualScopeSatisfier(ScopeTypeCache.MultiLineDefinition));
		}

		protected override void Execute(ParseContext context) {
			CloseContextualScopes(context);
			CloseCurrentScope(context);
		}

		public string Token { get { return "}:"; } }
	}
}
using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class OpenMultiLineDefinitionStrategy : ScopeDrivenStrategy, ITokenProvider {
		public OpenMultiLineDefinitionStrategy() {
			AddSatisfier<DependentSatisfier<DefinitionStrategy>>();
		}

		public override int Priority { get { return DefaultPriority - 1; } }

		protected override void Execute(ParseContext context) {
			OpenScope(new MultiLineDefinitionScope(), context);
		}

		public string Token { get { return ":{"; } }
	}

	public class CloseMultiLineDefinitionStrategy : ScopeDrivenStrategy, ITokenProvider {
		protected override void Execute(ParseContext context) {
			CloseCurrentScope(context);
		}

		public string Token { get { return "}:"; } }
	}
}
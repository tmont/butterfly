using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class OpenMultiLineDefinitionStrategy : ScopeDrivenStrategy {
		public OpenMultiLineDefinitionStrategy() {
			AddSatisfier<DependentSatisfier<DefinitionStrategy>>();
			AddSatisfier(new ExactCharMatchSatisfier(":{"));
		}

		public override int Priority { get { return DefaultPriority - 1; } }

		protected override void DoExecute(ParseContext context) {
			OpenScope(new MultiLineDefinitionScope(), context);
		}
	}

	public class CloseMultiLineDefinitionStrategy : ScopeDrivenStrategy {
		public CloseMultiLineDefinitionStrategy() {
			AddSatisfier(new ExactCharMatchSatisfier("}:"));
		}
		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}
	}
}
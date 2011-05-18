using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[NonDefault]
	public class OpenUnescapedStrategy : ScopeDrivenStrategy {
		public OpenUnescapedStrategy() {
			AddSatisfier(new ExactCharMatchSatisfier("[@"));
		}

		public override int Priority { get { return DefaultPriority - 1; } }
		
		protected override void DoExecute(ParseContext context) {
			OpenScope(new UnescapedScope(), context);
		}
	}
}
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[NonDefault]
	[TokenTransformer("[@")]
	public class OpenUnescapedStrategy : ScopeDrivenStrategy {
		public override int Priority { get { return DefaultPriority - 1; } }
		
		protected override void DoExecute(ParseContext context) {
			OpenScope(new UnescapedScope(), context);
		}
	}
}
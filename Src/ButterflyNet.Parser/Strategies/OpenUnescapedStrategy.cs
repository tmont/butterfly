using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[NonDefault]
	public class OpenUnescapedStrategy : ScopeDrivenStrategy, ITokenProvider {
		public override int Priority { get { return DefaultPriority - 1; } }
		public string Token { get { return "[@"; } }
		
		protected override void DoExecute(ParseContext context) {
			OpenScope(new UnescapedScope(), context);
		}
	}
}
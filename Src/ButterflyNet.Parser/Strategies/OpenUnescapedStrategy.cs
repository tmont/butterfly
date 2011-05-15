using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[NonDefault]
	public class OpenUnescapedStrategy : ScopeDrivenStrategy, ITokenProvider {
		public override int Priority { get { return DefaultPriority - 1; } }
		protected override bool Scopable { get { return false; } }
		public string Token { get { return "[@"; } }
		
		protected override void Execute(ParseContext context) {
			OpenScope(new UnescapedScope(), context);
		}
	}
}
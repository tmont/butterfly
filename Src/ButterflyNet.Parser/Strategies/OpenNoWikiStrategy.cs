using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class OpenNoWikiStrategy : ScopeDrivenStrategy, ITokenProvider {
		public override int Priority { get { return DefaultPriority - 1; } }

		protected override void DoExecute(ParseContext context) {
			OpenScope(new NoWikiScope(), context);
		}

		public string Token { get { return "[!"; } }
	}
}
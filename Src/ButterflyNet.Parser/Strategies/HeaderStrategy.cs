using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class HeaderStrategy : ScopeDrivenStrategy, ITokenProvider {
		public HeaderStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
		}

		protected override void Execute(ParseContext context) {
			var depth = 1;
			while (context.Input.Peek() == '!') {
				depth++;
				context.Input.Read();
			}

			context.Input.SeekToNonWhitespace(); //ignore spaces/tabs
			context.UpdateCurrentChar();
			OpenScope(new Scopes.HeaderScope(depth), context);
		}

		public string Token { get { return "!"; } }
	}
}